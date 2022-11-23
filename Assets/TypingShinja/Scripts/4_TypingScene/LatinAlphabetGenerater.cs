using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatinAlphabetGenerater
{
    string[] pieceOfLatinAlphabet;

    //ローマ字を生成して返すことがメインだが、同時にそのローマ字用のリストも作成し、ListsRelatedToLatinAlpabetに格納する
    public string GenerateLatinAlphabet(string ruby)
    {
        ListsRelatedToLatinAlphabet.Instance.ClearListsRelatedToLatinAlphabet();

        for (int i = 0; i < ruby.Length; i++)
        {
            pieceOfLatinAlphabet = LatinAlphabetDictionary.Instance.DictionaryOfLatinAlphabet[ruby[i].ToString()].ToArray();

            //文字が「ぁ」等であり、rubyの長さが1じゃない時は「SKIP」と上書きする
            if (ruby[i].ToString() == "ゃ" || ruby[i].ToString() == "ゅ" || ruby[i].ToString() == "ょ"
                || ruby[i].ToString() == "ぁ" || ruby[i].ToString() == "ぃ" || ruby[i].ToString() == "ぅ"
                || ruby[i].ToString() == "ぇ" || ruby[i].ToString() == "ぉ")
            {
                if (ruby.Length != 1)
                {
                    string[] array = { "SKIP" };
                    pieceOfLatinAlphabet = array;
                }
            }
            //文字が「っ」であり、それが最後の文字じゃない時、次のローマ字の１文字目を先頭に追加する（あっぷる→appuru等）
            else if (ruby[i].ToString() == "っ" && i + 1 < ruby.Length)
            {
                string[] array = { LatinAlphabetDictionary.Instance.DictionaryOfLatinAlphabet[ruby[i + 1].ToString()][0][0].ToString(), pieceOfLatinAlphabet[0], pieceOfLatinAlphabet[1] };
                pieceOfLatinAlphabet = array;
            }
            //文字が「ん」であり、それが最後の文字じゃない時であり、次の文字が母音またはやゆよじゃない時、nを二つ重ねる形で上書きする
            else if (ruby[i].ToString() == "ん" && i + 1 < ruby.Length)
            {
                if (ruby[i + 1].ToString() == "あ" || ruby[i + 1].ToString() == "い" || ruby[i + 1].ToString() == "う"
                    || ruby[i + 1].ToString() == "え" || ruby[i + 1].ToString() == "お" || ruby[i + 1].ToString() == "や"
                    || ruby[i + 1].ToString() == "ゆ" || ruby[i + 1].ToString() == "よ")
                {
                    string[] array = { "nn" };
                    pieceOfLatinAlphabet = array;
                }
            }
            //もし次の文字をくっつけた言葉が、日本語の音として辞書に登録されていた場合、それで上書きする（「にゃ」など）　最後の文字の場合は実行しない。
            else if (i + 1 < ruby.Length)
            {
                string addNextRuby = ruby[i].ToString() + ruby[i + 1].ToString();

                if (LatinAlphabetDictionary.Instance.DictionaryOfLatinAlphabet.ContainsKey(addNextRuby))
                {
                    pieceOfLatinAlphabet = LatinAlphabetDictionary.Instance.DictionaryOfLatinAlphabet[addNextRuby].ToArray();
                }
            }

            //ひらがな１文字ごとに、pieceOfLatinAlphabetを加えたSliceListを作る。
            //index系のリストも作成する
            ListsRelatedToLatinAlphabet.Instance.CreateListsRelatedToLatinAlphabet(pieceOfLatinAlphabet, i);
        }

        //SliceListからSKIPを抜いた文字列を取り出して返す
        return ListsRelatedToLatinAlphabet.Instance.GetLatinAlphabetStringWithoutSkip();
    }
}
