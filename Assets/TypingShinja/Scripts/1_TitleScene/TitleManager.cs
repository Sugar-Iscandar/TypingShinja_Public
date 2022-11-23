using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void ChangeToSaveDataSelectionScene()
    {
        SceneChanger.Instance.ChangeScene(Scenes.SaveDataSelection);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
