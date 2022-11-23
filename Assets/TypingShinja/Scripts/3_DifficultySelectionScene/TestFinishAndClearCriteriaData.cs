using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFinishAndClearCriteriaData
{
    FinishAndClearCriteria[] hoge = new FinishAndClearCriteria[21];

    public FinishAndClearCriteria[] Fuga
    {
        get => hoge;
        set => hoge = value;
    }

    public TestFinishAndClearCriteriaData()
    {
        for (int i = 0; i < hoge.Length; i++)
        {
            hoge[i] = new FinishAndClearCriteria();

            hoge[i].NumOfQuestions = 10;
            hoge[i].AllowableNumOfMistakes = 3;
            hoge[i].AllowableElapsedTime = 40.0f;
        }
    }
}
