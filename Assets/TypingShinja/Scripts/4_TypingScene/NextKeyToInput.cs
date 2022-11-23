using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class NextKeyToInput
{
    // 出題中のローマ字1文の何文字目かを表す
    // 例：ohayou -> {0,1,2,3,4,5}
    int indexOfLatinAlphabetQuestionCharacter = 0;
    // 出題中のふりがな１文の何文字目かを表す
    // 例：おはよう -> {0,1,2,3}
    int indexOfRubyQuestionCharacter = 0;
    // 現在のふりがなに対応したローマ字の候補配列
    // 例：「お」-> {o}
    // 例：「ちょ」-> {tyo,cho,cyo}
    string[] currentLatinAlphabetSliceArray;
    // 現在の”ふりがなに対応したローマ字候補（先頭）”のindex
    // 例：「お」-> o = {0}
    // 例：「ちょ」-> t,y,o = {0,1,2}
    int indexOfLatinAlphabetPiece;
    // 現在の入力候補のindex
    // 例：「お」-> o = {0}
    // 例：「ちょ」-> c,y,o = {0,1,2}
    int indexOfCurrentInputCondidate;
    // 直前のふりがなのindexを格納する
    int previousIndexOfRubyQuestionCharacter;
    // 出題中のローマ字を格納する
    string showingLatinAlphabet;
    // 次に打つべきキーのリスト
    // 例：「お」-> {o}
    // 例：「ちょ」-> {t,c,c}
    List<string> nextKeyToInputList = new List<string>();
    Difficulty currentDifficulty;
    Subject<string> nextKeySubject = new Subject<string>();
    Subject<Unit> oneQuestionFinishSubject = new Subject<Unit>();

    public int IndexOfLatinAlphabetQuestionCharactor
    {
        get => indexOfLatinAlphabetQuestionCharacter;
    }
    public int IndexOfCurrentInputCondidate
    {
        get => indexOfCurrentInputCondidate;
    }
    public string ShowingLatinAlphabet
    {
        get => showingLatinAlphabet;
    }
    public List<string> NextKeyToInputList
    {
        get => nextKeyToInputList;
    }
    public IObservable<string> OnNextKeyToInputGenerated
    {
        get => nextKeySubject;
    }
    public IObservable<Unit> OnOneQuestionFinished
    {
        get => oneQuestionFinishSubject;
    }

    public void Initialize(Difficulty dif)
    {
        currentDifficulty = dif;
    }

    public void AddOneIndexOfLatinAlphabetQuestionCharactor()
    {
        indexOfLatinAlphabetQuestionCharacter++;
    }

    public void SetCurrentInputCondidateIndex(int index)
    {
        indexOfCurrentInputCondidate = index;
    }

    // 次に打つべきキーを計算する
    public void CalculateNextKeyToInputList(string showingLatin)
    {
        showingLatinAlphabet = showingLatin;
        previousIndexOfRubyQuestionCharacter = 0;
        indexOfCurrentInputCondidate = 0;

        //1文を入力し終わってたら、1文の終了を知らせる
        if (indexOfLatinAlphabetQuestionCharacter >= showingLatin.Length)
        {
            // １文の終了を知らせる
            indexOfLatinAlphabetQuestionCharacter = 0;
            oneQuestionFinishSubject.OnNext(Unit.Default);
        }
        else
        {
            if (currentDifficulty <= Difficulty.Beginner5 || currentDifficulty == Difficulty.Intermediate5)
            {
                nextKeyToInputList.Clear();
                nextKeyToInputList.Add(showingLatin[indexOfLatinAlphabetQuestionCharacter].ToString());
                Debugger.Log("次に打つべきは" + nextKeyToInputList[0]);
                nextKeySubject.OnNext(nextKeyToInputList[0]);
            }
            else
            {
                indexOfRubyQuestionCharacter =
                ListsRelatedToLatinAlphabet
                .Instance
                .IndexOfRubyCharacterList[indexOfLatinAlphabetQuestionCharacter];

                currentLatinAlphabetSliceArray =
                    ListsRelatedToLatinAlphabet
                    .Instance
                    .LatinAlphabetSliceList[indexOfRubyQuestionCharacter];

                indexOfLatinAlphabetPiece =
                    ListsRelatedToLatinAlphabet
                    .Instance
                    .IndexOfLatinAlphabetPieceList[indexOfLatinAlphabetQuestionCharacter];

                nextKeyToInputList.Clear();

                // ローマ字候補の分だけ回す
                for (int i = 0; i < currentLatinAlphabetSliceArray.Length; i++)
                {
                    nextKeyToInputList.Add(currentLatinAlphabetSliceArray[i][indexOfLatinAlphabetPiece].ToString());
                    Debugger.Log("次に打つべきは" + nextKeyToInputList[i]);
                }
            }

            //購読するのはKeyViewおよびFingerView
            nextKeySubject.OnNext(nextKeyToInputList[0]);
        }
    }

    //次に打つべきキーを再計算する
    public void CalculateNextKeyToInputList(string showingLatin, bool isFlexibleInput, int indexOfMatchedNextKeyToInput)
    {
        showingLatinAlphabet = showingLatin;

        //1文を入力し終わってたら、1文の終了を知らせる
        if (indexOfLatinAlphabetQuestionCharacter >= showingLatin.Length)
        {
            // １文の終了を知らせる
            indexOfLatinAlphabetQuestionCharacter = 0;
            oneQuestionFinishSubject.OnNext(Unit.Default);
        }
        else
        {
            if (currentDifficulty <= Difficulty.Beginner5 || currentDifficulty == Difficulty.Intermediate5)
            {
                nextKeyToInputList.Clear();
                nextKeyToInputList.Add(showingLatin[indexOfLatinAlphabetQuestionCharacter].ToString());
                Debugger.Log("次に打つべきは" + nextKeyToInputList[0]);
                nextKeySubject.OnNext(nextKeyToInputList[0]);
            }
            else
            {
                indexOfRubyQuestionCharacter =
                ListsRelatedToLatinAlphabet
                .Instance
                .IndexOfRubyCharacterList[indexOfLatinAlphabetQuestionCharacter];

                currentLatinAlphabetSliceArray =
                    ListsRelatedToLatinAlphabet
                    .Instance
                    .LatinAlphabetSliceList[indexOfRubyQuestionCharacter];

                indexOfLatinAlphabetPiece =
                    ListsRelatedToLatinAlphabet
                    .Instance
                    .IndexOfLatinAlphabetPieceList[indexOfLatinAlphabetQuestionCharacter];

                nextKeyToInputList.Clear();

                //次のひらがなに移った時に、isFlexibleInputがtrueになっていると困る
                if (indexOfRubyQuestionCharacter != previousIndexOfRubyQuestionCharacter)
                {
                    isFlexibleInput = false;
                    indexOfCurrentInputCondidate = 0;
                }

                //ふりがなに対応したローマ字候補の分だけ回す
                for (int i = 0; i < currentLatinAlphabetSliceArray.Length; i++)
                {
                    //文字数が、次の入力候補の文字数より多かった場合は無条件でnull || 柔軟な入力があった場合、先頭の入力候補は絶対に再び打つことはない。
                    if (currentLatinAlphabetSliceArray[i].Length - 1 < indexOfLatinAlphabetPiece || isFlexibleInput && i == 0)
                    {
                        nextKeyToInputList.Add(null);
                        Debugger.Log("nullで上書き");
                    }
                    else
                    {
                        nextKeyToInputList.Add(currentLatinAlphabetSliceArray[i][indexOfLatinAlphabetPiece].ToString());
                        Debugger.Log("次に打つべきは" + nextKeyToInputList[i]);
                    }
                }

                for (int i = 0; i < nextKeyToInputList.Count; i++)
                {
                    if (nextKeyToInputList[i] != null)
                    {
                        nextKeySubject.OnNext(nextKeyToInputList[i]);
                        break;
                    }
                }

                previousIndexOfRubyQuestionCharacter = indexOfRubyQuestionCharacter;
            }
        }
    }

    public void ReCalculateNextKeyToInputList(bool isFlexibleInput, int indexOfMatchedNextKeyToInput)
    {
        CalculateNextKeyToInputList(showingLatinAlphabet, isFlexibleInput, indexOfMatchedNextKeyToInput);
    }

    public void ReCreateCurrentLatinAlphabetSliceList(int indexOfMatchedNextKeyToInput)
    {
        ListsRelatedToLatinAlphabet.Instance.OverwiteCurrentLatinAlphabetSliceList(indexOfRubyQuestionCharacter, indexOfMatchedNextKeyToInput);
    }

    public void ReCreateShowingLatinAlphabetAndIndexLists()
    {
        showingLatinAlphabet = ListsRelatedToLatinAlphabet.Instance.GetLatinAlphabetStringWithoutSkip();
        ListsRelatedToLatinAlphabet.Instance.ReCreateIndexLists();
    }

    public void CompleteStreamSouce()
    {
        nextKeySubject.OnCompleted();
        oneQuestionFinishSubject.OnCompleted();
    }
}
