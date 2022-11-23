using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    static SceneChanger instance;

    public static SceneChanger Instance
    {
        get => instance;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeScene(Scenes scene)
    {
        switch (scene)
        {
            case Scenes.Title:
                SceneManager.LoadScene("TitleScene");
                break;
            case Scenes.SaveDataSelection:
                SceneManager.LoadScene("SaveDataSelectionScene");
                break;
            case Scenes.DifficultySelection:
                SceneManager.LoadScene("DifficultySelectionScene");
                break;
            case Scenes.Typing:
                SceneManager.LoadScene("TypingScene");
                break;
        }
    }
}
