using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TitlePresenter : MonoBehaviour
{
    [SerializeField] TitleManager manager;
    [SerializeField] TitleView view;

    void Awake()
    {
        view.OnStartButtonClicked
            .Subscribe(_ =>
            {
                manager.ChangeToSaveDataSelectionScene();
            })
            .AddTo(this);

        view.OnQuitButtonClicked
            .Subscribe(_ =>
            {
                manager.QuitApplication();
            })
            .AddTo(this);
    }
}
