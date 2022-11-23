using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OverallDifficultyButton : MonoBehaviour, IOverallDifficultyButton
{
    [SerializeField] OverallDifficulty overallDifficulty;

    Button thisButton;

    UnityAction<OverallDifficulty> onButtonClicked = null;

    public UnityAction<OverallDifficulty> OnButtonClicked
    {
        get => onButtonClicked;
        set => onButtonClicked = value;
    }

    void Awake()
    {
        thisButton = this.gameObject.GetComponent<Button>();

        thisButton.onClick.AddListener(() => onButtonClicked?.Invoke(overallDifficulty));

        thisButton.interactable = false;
    }

    public void DetermineWhetherToEnableButton(OverallDifficulty overallDif)
    {
        if (overallDif >= overallDifficulty)
        {
            thisButton.interactable = true;
        }
    }
}
