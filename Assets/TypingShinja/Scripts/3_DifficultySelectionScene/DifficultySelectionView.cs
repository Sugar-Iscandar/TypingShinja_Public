using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DifficultySelectionView : MonoBehaviour
{
    [SerializeField] GameObject panels;
    [SerializeField] Button buttonGoTitle;
    [SerializeField] Button[] buttonsBack;
    IOverallDifficultyButton[] interfaceOfOverallDifficultyButton;
    IDifficultyButton[] interfaceOfDifficultyButton;
    public event UnityAction OnGoTitleButtonClicked = null;
    public event UnityAction<Difficulty> OnDifficultyButtonClicked = null;

    public void Initialize(SaveData saveData)
    {
        buttonGoTitle.onClick.AddListener(() => OnGoTitleButtonClicked?.Invoke());

        foreach (Button button in buttonsBack)
        {
            button.onClick.AddListener(() => ResetPanelPosition());
        }

        interfaceOfOverallDifficultyButton
            = GameObjectExtensions.FindObjectsOfInterface<IOverallDifficultyButton>();
        interfaceOfDifficultyButton
            = GameObjectExtensions.FindObjectsOfInterface<IDifficultyButton>();

        foreach (IOverallDifficultyButton iOverallButton in interfaceOfOverallDifficultyButton)
        {
            iOverallButton.OnButtonClicked += (overallDif) => MovePanel(overallDif);
        }

        foreach (IDifficultyButton iDifButton in interfaceOfDifficultyButton)
        {
            iDifButton.OnButtonClicked += (dif) => OnDifficultyButtonClicked?.Invoke(dif);
        }

        ButtonEnable(saveData);
    }

    void ButtonEnable(SaveData saveData)
    {
        foreach (IOverallDifficultyButton iOverallButton in interfaceOfOverallDifficultyButton)
        {
            iOverallButton.DetermineWhetherToEnableButton(saveData.OverallDifficulty);
        }

        foreach (IDifficultyButton iDifButton in interfaceOfDifficultyButton)
        {
            iDifButton.DetermineWhetherToEnableButton(saveData.Difficulty);
        }
    }

    void MovePanel(OverallDifficulty overallDifficulty)
    {
        panels.transform.localPosition = new Vector3(((int)overallDifficulty + 1) * -2000, 0, 0);
    }

    void ResetPanelPosition()
    {
        panels.transform.localPosition = new Vector3(0, 0, 0);
    }
}
