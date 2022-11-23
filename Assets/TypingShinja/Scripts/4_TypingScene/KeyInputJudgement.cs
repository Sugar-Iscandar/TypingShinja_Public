using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyInputJudgement
{
    NextKeyToInput nextKeyToInput;
    int missCount;
    public event UnityAction<int> OnCorrect = null;
    public event UnityAction<string, int> OnFlexibleCorrect = null;
    public event UnityAction<int, int> OnMiss = null;

    public int MissCount
    {
        get => missCount;
    }

    //マネージャーからNextKeyToInputのインスタンスをもらう
    public void Initialize(NextKeyToInput instance)
    {
        nextKeyToInput = instance;
        missCount = 0;
    }

    public void JudgmentKeyInput(string inputKey)
    {
        if (inputKey == "leftshift" || inputKey == "rightshift") return;
        
        bool isFlexibleCorrect = false;

        // 先頭（デフォルト表示）もしくは、柔軟入力に切り替わった続きのローマ字で正解した場合
        if (inputKey == nextKeyToInput.NextKeyToInputList[nextKeyToInput.IndexOfCurrentInputCondidate])
        {
            Debugger.Log("正解！");
            nextKeyToInput.AddOneIndexOfLatinAlphabetQuestionCharactor();
            OnCorrect?.Invoke(nextKeyToInput.IndexOfLatinAlphabetQuestionCharactor);
            nextKeyToInput.ReCalculateNextKeyToInputList(isFlexibleCorrect, nextKeyToInput.IndexOfCurrentInputCondidate);
        }
        //複数の入力候補が存在していた場合
        else if (nextKeyToInput.NextKeyToInputList.Count >= 2)
        {
            //入力候補の数だけ回す
            for (int i = 0; i < nextKeyToInput.NextKeyToInputList.Count; i++)
            {
                //入力候補の一つと一致していた場合
                if (inputKey == nextKeyToInput.NextKeyToInputList[i])
                {
                    Debugger.Log("柔軟な正解！");
                    isFlexibleCorrect = true;

                    nextKeyToInput.SetCurrentInputCondidateIndex(i);

                    //このバージョンの新しいcurrentSliceListを作成する
                    nextKeyToInput.ReCreateCurrentLatinAlphabetSliceList(i);

                    //showingを再生成する
                    //Index系のリストを再生成する
                    nextKeyToInput.ReCreateShowingLatinAlphabetAndIndexLists();

                    //正解なので、メインindexを＋１
                    nextKeyToInput.AddOneIndexOfLatinAlphabetQuestionCharactor();

                    //柔軟な入力イベントを発行。Viewに連絡し、表示しているローマ字を柔軟バージョンに対応させる。
                    OnFlexibleCorrect?.Invoke(nextKeyToInput.ShowingLatinAlphabet, nextKeyToInput.IndexOfLatinAlphabetQuestionCharactor);

                    nextKeyToInput.ReCalculateNextKeyToInputList(isFlexibleCorrect, i);
                }
            }

            if (!isFlexibleCorrect)
            {
                Debugger.Log("柔軟も失敗");
                missCount++;
                OnMiss?.Invoke(nextKeyToInput.IndexOfLatinAlphabetQuestionCharactor, missCount);
            }
        }
        else
        {
            Debugger.Log("失敗");
            missCount++;
            OnMiss?.Invoke(nextKeyToInput.IndexOfLatinAlphabetQuestionCharactor, missCount);
        }
    }
}
