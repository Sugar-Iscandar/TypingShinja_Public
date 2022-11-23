using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class UnshownQuestionCounter
{
    ReactiveProperty<int> unshownQuestionCount = new ReactiveProperty<int>();
    bool isSetable = true;

    public int UnshownQuestionCount
    {
        get => unshownQuestionCount.Value;
        set
        {
            if (!isSetable) return;
            unshownQuestionCount.Value = value;
            isSetable = false;
        }
    }

    public IObservable<int> OnUnshownQuestionCountChanged
    {
        get => unshownQuestionCount;
    }

    public void ReduceUnshownQuestionCount()
    {
        unshownQuestionCount.Value--;
    }

    public void CompleteStreamSouce()
    {
        unshownQuestionCount.Dispose();
    }
}
