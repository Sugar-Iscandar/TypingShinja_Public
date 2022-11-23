using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListsRelatedToLatinAlphabet
{
    static ListsRelatedToLatinAlphabet instance = new ListsRelatedToLatinAlphabet();

    public static ListsRelatedToLatinAlphabet Instance
    {
        get { return instance; }
    }

    private ListsRelatedToLatinAlphabet() { }

    List<string> currentLatinAlphabetSliceList = new List<string>();

    List<string[]> latinAlphabetSliceList = new List<string[]>();

    //ローマ字の数だけ、ローマ字に対応したふりがなのindex番号が格納される。
    List<int> indexOfRubyCharacterList = new List<int>();

    List<int> indexOfLatinAlphabetPieceList = new List<int>();

    public List<string[]> LatinAlphabetSliceList
    {
        get { return latinAlphabetSliceList; }
    }

    public List<int> IndexOfRubyCharacterList
    {
        get { return indexOfRubyCharacterList; }
    }

    public List<int> IndexOfLatinAlphabetPieceList
    {
        get { return indexOfLatinAlphabetPieceList; }
    }

    public void ClearListsRelatedToLatinAlphabet()
    {
        currentLatinAlphabetSliceList.Clear();
        latinAlphabetSliceList.Clear();
        indexOfRubyCharacterList.Clear();
        indexOfLatinAlphabetPieceList.Clear();
    }

    //全てのリストをセットで作る。最初に実行される。
    public void CreateListsRelatedToLatinAlphabet(string[] pieceOfLatinAlphabet, int index)
    {
        latinAlphabetSliceList.Add(pieceOfLatinAlphabet);

        currentLatinAlphabetSliceList.Add(pieceOfLatinAlphabet[0]);
        Debug.Log(pieceOfLatinAlphabet[0]);

        if (pieceOfLatinAlphabet[0] != "SKIP")
        {
            for (int j = 0; j < pieceOfLatinAlphabet[0].Length; j++)
            {
                indexOfRubyCharacterList.Add(index);
                indexOfLatinAlphabetPieceList.Add(j);
            }
        }
    }

    //currentLatinAlphabetSliceListより、SKIPを除いた文字列を生成する。
    public string GetLatinAlphabetStringWithoutSkip()
    {
        List<string> resultList = new List<string>();

        foreach (string latinSlice in currentLatinAlphabetSliceList)
        {
            if (latinSlice == "SKIP")
            {
                continue;
            }
            resultList.Add(latinSlice);
        }

        return string.Join("", resultList);
    }

    public void OverwiteCurrentLatinAlphabetSliceList(int indexOfRuby, int indexOfArray)
    {
        for (int i = 0; i < latinAlphabetSliceList.Count; i++)
        {
            if (i == indexOfRuby)
            {
                currentLatinAlphabetSliceList[i] = latinAlphabetSliceList[i][indexOfArray];
            }
        }
    }

    public void ReCreateIndexLists()
    {
        indexOfRubyCharacterList.Clear();
        indexOfLatinAlphabetPieceList.Clear();

        for (int i = 0; i < currentLatinAlphabetSliceList.Count; i++)
        {
            string currentLatin = currentLatinAlphabetSliceList[i];
            
            if (currentLatin == "SKIP")
            {
                continue;
            }

            for (int j = 0; j < currentLatin.Length; j++)
            {
                indexOfRubyCharacterList.Add(i);
                indexOfLatinAlphabetPieceList.Add(j);
            }
        }

        Debug.Log("作り替えたrubyList" + string.Join(",", indexOfRubyCharacterList));
        Debug.Log("作り替えたlatinList" + string.Join(",", indexOfLatinAlphabetPieceList));

    }
}
