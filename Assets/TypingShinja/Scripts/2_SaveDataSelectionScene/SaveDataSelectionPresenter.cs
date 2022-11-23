using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataSelectionPresenter : MonoBehaviour
{
    [SerializeField] SaveDataSelectionManager manager;
    [SerializeField] SaveDataSelectionView view;

    void Awake()
    {
        view.OnButtonSaveDataClicked += (saveNum) => manager.LoadSaveData(saveNum);
        view.OnButtonBackClicked += () => manager.ChangeToTitleScene();
    }
}
