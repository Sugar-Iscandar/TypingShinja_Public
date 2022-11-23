using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentlyLoadedSaveData
{
    static CurrentlyLoadedSaveData instance = new CurrentlyLoadedSaveData();

    public static CurrentlyLoadedSaveData Instance
    {
        get { return instance; }
    }

    private CurrentlyLoadedSaveData() { }

    SaveData currentSaveData = new SaveData();

    public SaveData CurrentSaveData
    {
        get => currentSaveData;
        set => currentSaveData = value;
    }
}
