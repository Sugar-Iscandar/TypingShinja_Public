using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SaveDataSelectionView : MonoBehaviour
{
    [SerializeField] Button buttonSaveData1;
    [SerializeField] Button buttonSaveData2;
    [SerializeField] Button buttonSaveData3;
    [SerializeField] Button buttonBack;

    public event UnityAction<int> OnButtonSaveDataClicked = null;
    public event UnityAction OnButtonBackClicked = null;

    void Awake()
    {
        buttonSaveData1.onClick.AddListener(() => OnButtonSaveDataClicked?.Invoke(0));
        buttonSaveData2.onClick.AddListener(() => OnButtonSaveDataClicked?.Invoke(1));
        buttonSaveData3.onClick.AddListener(() => OnButtonSaveDataClicked?.Invoke(2));
        buttonBack.onClick.AddListener(() => OnButtonBackClicked?.Invoke());
    }
}
