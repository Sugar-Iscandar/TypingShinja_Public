using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UniRx;
using System;

public class DialogView : MonoBehaviour
{
    [SerializeField] GameObject startDialog;
    [SerializeField] GameObject finishDialog;
    [SerializeField] Text textClearCriteria;
    [SerializeField] Text textElapsedTimeResult;
    [SerializeField] Text textMissNumResult;
    [SerializeField] Text textPassFailResult;
    [SerializeField] Button buttonRetry;
    [SerializeField] Button buttonReSelectDifficulty;

    public bool StartDialogStatus
    {
        set => startDialog.SetActive(value);
    }
    public bool FinishDialogStatus
    {
        set => finishDialog.SetActive(value);
    }

    public IObservable<Unit> OnRetryButtonClicked
    {
        get => buttonRetry.OnClickAsObservable();
    }
    public IObservable<Unit> OnReSelectDifficultyButtonClicked
    {
        get => buttonReSelectDifficulty.OnClickAsObservable();
    }

    public void Initialize(Difficulty dif, FinishAndClearCriteria criteria)
    {
        if (dif < Difficulty.Advanced1)
        {
            textClearCriteria.text = "ミス " + criteria.AllowableNumOfMistakes.ToString() + " 回以内";
        }
        else
        {
            textClearCriteria.text = ((int)criteria.AllowableElapsedTime).ToString() + "　秒以内にフィニッシュ";
        }
        StartDialogStatus = true;
        textElapsedTimeResult.enabled = false;
        textMissNumResult.enabled = false;
        textPassFailResult.enabled = false;
    }

    public void ShowFinishResult(float elapsedTimeResult, int MissNumResult, bool isPassResult)
    {
        textElapsedTimeResult.text = ((int)elapsedTimeResult).ToString() + "秒";
        textMissNumResult.text = MissNumResult.ToString() + "回";

        if (isPassResult)
        {
            textPassFailResult.text = "合格";
        }
        else
        {
            textPassFailResult.text = "不合格";
        }
        StartCoroutine(ShowResult());
    }

    IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(0.5f);

        textElapsedTimeResult.enabled = true;

        yield return new WaitForSeconds(0.5f);

        textMissNumResult.enabled = true;

        yield return new WaitForSeconds(0.5f);

        textPassFailResult.enabled = true;
    }
}
