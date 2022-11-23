using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameJudgment
{
    public bool JudgmentResult(int missNum, float elapsedSeconds, Difficulty dif, FinishAndClearCriteria criteria)
    {
        bool isPass;

        if (dif < Difficulty.Advanced1)
        {
            if (missNum <= criteria.AllowableNumOfMistakes)
            {
                isPass = true;
            }
            else
            {
                isPass = false;
            }
        }
        else
        {
            if (elapsedSeconds <= criteria.AllowableElapsedTime)
            {
                isPass = true;
            }
            else
            {
                isPass = false;
            }
        }

        return isPass;
    }
}
