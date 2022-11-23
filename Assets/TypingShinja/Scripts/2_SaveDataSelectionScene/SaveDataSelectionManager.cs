using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataSelectionManager : MonoBehaviour
{
    //セーブデータからデータを読み出し、CurrentlyLoadedSaveDataへ書き出す
    //今はテストなので直書き
    public void LoadSaveData(int saveDataNum)
    {
        CurrentlyLoadedSaveData
            .Instance
            .CurrentSaveData
            .SaveDataNum = saveDataNum;
        CurrentlyLoadedSaveData
            .Instance
            .CurrentSaveData
            .OverallDifficulty = OverallDifficulty.Special;
        CurrentlyLoadedSaveData
            .Instance
            .CurrentSaveData
            .Difficulty = Difficulty.Special5;

        ChangeToDifficultySelectionScene();
    }

    void ChangeToDifficultySelectionScene()
    {
        SceneChanger.Instance.ChangeScene(Scenes.DifficultySelection);
    }

    public void ChangeToTitleScene()
    {
        SceneChanger.Instance.ChangeScene(Scenes.Title);
    }
}
