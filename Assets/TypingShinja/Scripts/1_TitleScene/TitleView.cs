using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class TitleView : MonoBehaviour
{
    [SerializeField] Button buttonStart;
    [SerializeField] Button buttonQuit;

    public IObservable<Unit> OnStartButtonClicked
    {
        get => buttonStart.OnClickAsObservable();
    }

    public IObservable<Unit> OnQuitButtonClicked
    {
        get => buttonQuit.OnClickAsObservable();
    }
}
