using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class TypingPresenter : MonoBehaviour
{
    [SerializeField] TypingManager manager;
    [SerializeField] StatusView statusView;
    [SerializeField] DialogView dialogView;
    [SerializeField] QuestionView questionView;
    [SerializeField] CountdownView countdownView;
    [SerializeField] KeyView keyView;
    [SerializeField] FingerView fingerView;
    void Awake()
    {
        manager.OnInitialize += (dif,criteria) =>
        {
            statusView.Initialize();
            dialogView.Initialize(dif,criteria);
            questionView.Initialize();
            countdownView.Initialize();
        };

        // managerがカウントダウン終了後にOnCompletedを発行
        manager.OnCountdownProgress
            .Take(1)
            .Subscribe(_ =>
            {
                dialogView.StartDialogStatus = false;
                countdownView.CountdownTextStatus = true;
            });
        manager.OnCountdownProgress
            .Where(count => count != 0)
            .Subscribe(count =>
            {
                countdownView.SetCountdownText(count);
            });
        manager.OnCountdownProgress
            .Where(count => count == 0)
            .Subscribe(_ =>
            {
                countdownView.CountdownTextStatus = false;
                countdownView.StartTextStatus = true;
            });
        manager.OnCountdownProgress
            .DoOnCompleted(() =>
            {
                countdownView.StartTextStatus = false;
            })
            .Subscribe();
        
        //Finish時にmanagerがストリームを停止させる
        manager.OnShowingQuestionChanged
            .Subscribe(question => 
            {
                questionView.SetQuestionText(question);
            });
        manager.OnElapsedSecondsChanged
            .Subscribe(second =>
            {
                statusView.ChangeElapsedSecondsText(second);
            });
        manager.OnUnshownQuestionCountChanged
            .Subscribe(unshownCount => 
            {
                statusView.ChangeUnshownQuestionNumText(unshownCount);
            });
        manager.OnUnshownQuestionCountChanged
            .Where(unshowunCount => unshowunCount == 0)
            .Subscribe(unshownCount =>
            {
                countdownView.FinishTextStatus = true;
            });
        manager.OnNextKeyToInputGenerated
            .Subscribe(key => 
            {
                keyView.InActiveKeyPieceOfPrevious();
                fingerView.InactiveFingerPieceOfPrevious();
                keyView.ActiveKeyPieceOf(key);
                fingerView.ActiveFingerPieceOf(key);
            });

        manager.OnInputCorrect = (index) => questionView.ChangeTextColorCorrect(index);
        manager.OnInputFlexibleCorrect = (latin,index) =>
        {
            questionView.SetLatinAlphabetQuestionText(latin);
            questionView.ChangeTextColorCorrect(index);
        };
        manager.OnInputMiss = (index,missNum) =>
        {
            questionView.ChangeTextColorIncorrect(index);
            statusView.ChangeMissNumText(missNum);
        };

        manager.OnOneQuestionFinished
            .Subscribe(_ =>
            {
                statusView.ShowCorrectCircle();
            });

        manager.OnGameFinished += (missCount,seconds,isPass) => 
        {
            countdownView.FinishTextStatus = false;
            dialogView.FinishDialogStatus = true;
            dialogView.ShowFinishResult(seconds,missCount,isPass);
        };

        dialogView.OnRetryButtonClicked
            .Subscribe(_ => 
            {
                manager.ReloadThisScene();
            })
            .AddTo(this);
        dialogView.OnReSelectDifficultyButtonClicked
            .Subscribe(_ => 
            {
                manager.ChangeToDifficultySelectScene();
            })
            .AddTo(this);
    }
}
