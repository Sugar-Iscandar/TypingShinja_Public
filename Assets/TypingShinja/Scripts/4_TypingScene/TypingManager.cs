using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;
using System;

public class TypingManager : MonoBehaviour
{
    /* 状態 */
    Difficulty currentDifficulty;
    FinishAndClearCriteria currentCriteria;
    bool isGameStarted = false;

    /* ロジッククラス */
    KeyInputObserver keyInputObserver;
    OneQuestionGenerator oneQuestionGenerator = new OneQuestionGenerator();
    Timer timer = new Timer();
    NextKeyToInput nextKeyToInput = new NextKeyToInput();
    KeyInputJudgement keyInputJudgement = new KeyInputJudgement();
    UnshownQuestionCounter unshownQuestionCounter = new UnshownQuestionCounter();
    GameJudgment gameJudgment = new GameJudgment();

    /* イベントソース */
    public event UnityAction<Difficulty, FinishAndClearCriteria> OnInitialize = null;
    Subject<int> countdownSubject = new Subject<int>();
    public event UnityAction<int,float,bool> OnGameFinished = null;

    /* ストリーム停止用Dispose */
    IDisposable spaceEnterStream;

    /* Viewが購読・利用するイベント */
    public IObservable<int> OnCountdownProgress
    {
        get => countdownSubject;
    }
    public IObservable<int> OnElapsedSecondsChanged
    {
        get => timer.OnElapsedSecondsChanged;
    }
    public IObservable<ShowingQuestion> OnShowingQuestionChanged
    {
        get => oneQuestionGenerator.OnShowingQuestionGenerated;
    }
    public IObservable<string> OnNextKeyToInputGenerated
    {
        get => nextKeyToInput.OnNextKeyToInputGenerated;
    }
    public UnityAction<int> OnInputCorrect
    {
        set => keyInputJudgement.OnCorrect += value;
    }
    public UnityAction<string,int> OnInputFlexibleCorrect
    {
        set => keyInputJudgement.OnFlexibleCorrect += value;
    }
    public UnityAction<int,int> OnInputMiss
    {
        set => keyInputJudgement.OnMiss += value;
    }
    public IObservable<Unit> OnOneQuestionFinished
    {
        get => nextKeyToInput.OnOneQuestionFinished;
    }
    public IObservable<int> OnUnshownQuestionCountChanged
    {
        get => unshownQuestionCounter.OnUnshownQuestionCountChanged;
    }
    
    void Start()
    {
        keyInputJudgement.Initialize(nextKeyToInput);
        currentDifficulty = CurrentDifficulty.Instance.Difficulty;
        currentCriteria = CurrentFinishAndClearCriteria.Instance.Criteria;
        OnInitialize?.Invoke(currentDifficulty, currentCriteria);
        nextKeyToInput.Initialize(currentDifficulty);
        unshownQuestionCounter.UnshownQuestionCount = currentCriteria.NumOfQuestions;
        keyInputObserver = GetComponent<KeyInputObserver>();
        //StartStartProcess内でDispose
        spaceEnterStream = keyInputObserver.OnAnyKeyEntered
            .Where(key => key == "space" && isGameStarted == false)
            .Take(1)
            .Subscribe(_ => 
            {
                StartStartProcess();
            });
        //Finish時にOnCompleted
        keyInputObserver.OnAnyKeyEntered
            .Where(_ => isGameStarted == true)
            .Subscribe(key => 
            {
                OnAnyKeyEntered(key);
            });
        nextKeyToInput.OnOneQuestionFinished
            .Subscribe(_ => 
            {
                GenerateNextOneQuestion();
            });
        unshownQuestionCounter.OnUnshownQuestionCountChanged
            .Where(count => count == 0)
            .Subscribe(_ => 
            {
                FinishGame();
            });
    }

    //ゲーム開始前にスペースキーが押されたら実行
    void StartStartProcess()
    {
        spaceEnterStream.Dispose();
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        countdownSubject.OnNext(3);
        yield return new WaitForSeconds(1.0f);
        countdownSubject.OnNext(2);
        yield return new WaitForSeconds(1.0f);
        countdownSubject.OnNext(1);
        yield return new WaitForSeconds(1.0f);
        countdownSubject.OnNext(0);
        yield return new WaitForSeconds(1.0f);
        countdownSubject.OnCompleted();
        StartGame();
    }

    void StartGame()
    {
        //１文を生成
        oneQuestionGenerator.GenerateShowingQuestion(currentDifficulty);
        //次に押されるべきキーを計算
        nextKeyToInput.CalculateNextKeyToInputList(oneQuestionGenerator.ShowingQuestion.Latin);
        timer.StartCalculateTime();
        isGameStarted = true;
    }

    //ゲーム中にキーが押されたら実行される
    void OnAnyKeyEntered(string key)
    {
        keyInputJudgement.JudgmentKeyInput(key);
    }

    // 一文に正解したら
    void GenerateNextOneQuestion()
    {
        unshownQuestionCounter.ReduceUnshownQuestionCount();
        oneQuestionGenerator.GenerateShowingQuestion(currentDifficulty);
        nextKeyToInput.CalculateNextKeyToInputList(oneQuestionGenerator.ShowingQuestion.Latin);
    }

    //残り問題数が0になったら実行
    void FinishGame()
    {
        //タイピング処理に関連する全てのストリームを停止
        keyInputObserver.CompleteStreamSouce();
        oneQuestionGenerator.CompleteStreamSouce();
        timer.StopCalculateTime();
        nextKeyToInput.CompleteStreamSouce();
        unshownQuestionCounter.CompleteStreamSouce();
        //結果を判定
        bool isPass = gameJudgment.JudgmentResult
            (
                keyInputJudgement.MissCount,
                timer.ElapsedSeconds,
                currentDifficulty,
                currentCriteria
            );
        //一定時間待機後、ダイアログ表示命令
        Observable.Timer(System.TimeSpan.FromSeconds(2))
            .Subscribe(_ => 
            {
                OnGameFinished?.Invoke(keyInputJudgement.MissCount,timer.ElapsedSeconds,isPass);
            });
    }

    public void ReloadThisScene()
    {
        SceneChanger.Instance.ChangeScene(Scenes.Typing);
    }

    public void ChangeToDifficultySelectScene()
    {
        SceneChanger.Instance.ChangeScene(Scenes.DifficultySelection);
    }
}
