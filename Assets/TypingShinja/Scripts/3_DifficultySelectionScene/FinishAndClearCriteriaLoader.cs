using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FinishAndClearCriteriaLoader
{
    public FinishAndClearCriteria LoadCriteria(Difficulty difficulty)
    {
        OverallDifficulty overallDifficulty = OverallDifficulty.Beginner;

        if (difficulty <= Difficulty.Beginner5)
        {
            overallDifficulty = OverallDifficulty.Beginner;
        }
        else if (difficulty <= Difficulty.Intermediate6)
        {
            overallDifficulty = OverallDifficulty.Intermediate;
        }
        else if (difficulty <= Difficulty.Advanced5)
        {
            overallDifficulty = OverallDifficulty.Advanced;
        }
        else if (difficulty <= Difficulty.Special6)
        {
            overallDifficulty = OverallDifficulty.Special;
        }

        //読み込むファイルを決定する
        string fileName = "CriteriaData_" + ((int)overallDifficulty).ToString();
        Debugger.Log("読み出すファイル：" + fileName);
        FinishAndClearCriteria criteria = new FinishAndClearCriteria();
        string[] contents = null;
        TextAsset questionCSV = Resources.Load("CriteriaData/" + fileName) as TextAsset;
        StringReader reader = new StringReader(questionCSV.text);
        //1行目は無視
        reader.ReadLine();
        while (reader.Peek() > -1) 
        {
            string line = reader.ReadLine();
            contents = line.Split(',');
            if (((int)difficulty).ToString() == contents[0]) break;
        }

        criteria.NumOfQuestions = int.Parse(contents[1]);
        if (overallDifficulty <= OverallDifficulty.Intermediate)
        {
            criteria.AllowableNumOfMistakes = int.Parse(contents[2]);
        }
        else
        {
            criteria.AllowableElapsedTime = int.Parse(contents[2]);
        }

        return criteria;
    }
}
