using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DifficultySelectionManager : MonoBehaviour
{
    QuestionLoader questionLoader = new QuestionLoader();
    FinishAndClearCriteriaLoader finishAndClearCriteriaLoader = new FinishAndClearCriteriaLoader();
    public event UnityAction<SaveData> OnInitialize = null;

    // Start is called before the first frame update
    void Start()
    {
        SaveData currentSaveData;
        currentSaveData = CurrentlyLoadedSaveData.Instance.CurrentSaveData;
        OnInitialize?.Invoke(currentSaveData);
    }

    public void LoadFinishAndClearCriteriaData(Difficulty difficulty)
    {
        CurrentFinishAndClearCriteria
            .Instance
            .Criteria = finishAndClearCriteriaLoader.LoadCriteria(difficulty);
    }

    public void LoadQuestionData(Difficulty difficulty)
    {
        CurrentQuestions.Instance.Questions = questionLoader.LoadQuestion(difficulty);
        CurrentDifficulty.Instance.Difficulty = difficulty;
        ChangeToTypingScene();
    }

    public void ChangeToTitleScene()
    {
        SceneChanger.Instance.ChangeScene(Scenes.Title);
    }

    void ChangeToTypingScene()
    {
        SceneChanger.Instance.ChangeScene(Scenes.Typing);
    }
}
