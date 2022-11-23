using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelectionPresenter : MonoBehaviour
{
    [SerializeField] DifficultySelectionManager manager;
    [SerializeField] DifficultySelectionView view;

    void Awake()
    {
        manager.OnInitialize += (saveData) => view.Initialize(saveData);
        view.OnGoTitleButtonClicked += () => manager.ChangeToTitleScene();
        // ↓実行順に依存
        view.OnDifficultyButtonClicked += (dif) => manager.LoadFinishAndClearCriteriaData(dif);
        view.OnDifficultyButtonClicked += (dif) => manager.LoadQuestionData(dif);
    }
}
