using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusView : MonoBehaviour
{
    [SerializeField] Text textElapsedSeconds;
    [SerializeField] Text textUnshownQuestionNum;
    [SerializeField] Text textMissNum;
    [SerializeField] Text textCorrectCircle;

    public void Initialize()
    {
        textElapsedSeconds.text = "0";
        textUnshownQuestionNum.text = "0";
        textMissNum.text = "0";
        textCorrectCircle.enabled = false;
    }

    public void ChangeElapsedSecondsText(int newElapsedSeconds)
    {
        textElapsedSeconds.text = newElapsedSeconds.ToString();
    }

    public void ChangeUnshownQuestionNumText(int newNum)
    {
        textUnshownQuestionNum.text = newNum.ToString();
    }

    public void ChangeMissNumText(int newNum)
    {
        Debugger.Log(newNum);
        textMissNum.text = newNum.ToString();
    }

    public void ShowCorrectCircle()
    {
        StartCoroutine(ShowCircle());
    }

    IEnumerator ShowCircle()
    {
        textCorrectCircle.enabled = true;
        yield return new WaitForSeconds(0.3f);
        textCorrectCircle.enabled = false;
    }
}
