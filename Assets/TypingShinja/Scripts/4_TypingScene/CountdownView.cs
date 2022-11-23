using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownView : MonoBehaviour
{
    [SerializeField] Text textCountdown;
    [SerializeField] Text textStart;
    [SerializeField] Text textFinish;
    
    public bool CountdownTextStatus
    {
        set => textCountdown.enabled = value;
    }

    public bool StartTextStatus
    {
        set => textStart.enabled = value;
    }

    public bool FinishTextStatus
    {
        set => textFinish.enabled = value;
    }

    public void Initialize()
    {
        CountdownTextStatus = false;
        StartTextStatus = false;
        FinishTextStatus = false;
        textCountdown.text = "3";
    }

    public void SetCountdownText(int newCountdownNum)
    {
        textCountdown.text = newCountdownNum.ToString();
    }
}
