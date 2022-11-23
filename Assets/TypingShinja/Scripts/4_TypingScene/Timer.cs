using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class Timer
{
    float elapsedSeconds = 0.0f;
    float previousElapsedSeconds;

    Subject<int> timerSubject = new Subject<int>();

    public float ElapsedSeconds
    {
        get => elapsedSeconds;
    }
    public IObservable<int> OnElapsedSecondsChanged
    {
        get => timerSubject;
    }

    IDisposable timerStream;

    public void StartCalculateTime()
    {
        elapsedSeconds = 0.0f;
        timerStream = Observable.EveryUpdate()
            .Subscribe(_ => 
            {
                elapsedSeconds += Time.deltaTime;

                if ((int)elapsedSeconds != (int)previousElapsedSeconds)
                {
                    timerSubject.OnNext((int)elapsedSeconds);
                }

                previousElapsedSeconds = elapsedSeconds;
            });
    }

    public void StopCalculateTime()
    {
        timerStream.Dispose();
        timerSubject.OnCompleted();
    }
}
