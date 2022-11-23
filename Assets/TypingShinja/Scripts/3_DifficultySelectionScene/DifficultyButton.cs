using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DifficultyButton : MonoBehaviour, IDifficultyButton
{
    [SerializeField] Difficulty difficulty;

    Button thisButton;

    UnityAction<Difficulty> onButtonClicked = null;

    public UnityAction<Difficulty> OnButtonClicked
    {
        get => onButtonClicked;
        set => onButtonClicked = value;
    }

    void Awake()
    {
        thisButton = this.gameObject.GetComponent<Button>();

        thisButton.onClick.AddListener(() => onButtonClicked?.Invoke(difficulty));

        thisButton.interactable = false;
    }

    public void DetermineWhetherToEnableButton(Difficulty dif)
    {
        if (dif >= difficulty)
        {
            thisButton.interactable = true;
        }
    }
}
