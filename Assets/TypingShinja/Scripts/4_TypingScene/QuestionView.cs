using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionView : MonoBehaviour
{
    [SerializeField] Text textRubyQuestion;
    [SerializeField] Text textDefaultQuestion;
    [SerializeField] Text textLatinAlphabetQuestion;
    string colorGreen = "<color=#56D951>";
    string colorRed = "<color=#FF0000>";
    string colorEnd = "</color>";
    string showingLatinAlphabet;

    public void Initialize()
    {
        textRubyQuestion.text = "";
        textDefaultQuestion.text = "";
        textLatinAlphabetQuestion.text = "";
    }

    public void SetQuestionText(ShowingQuestion question)
    {
        if (question.Def.Length >= 7)
        {
            textRubyQuestion.text = "<size=40>" + question.Ruby + "</size>";;
            textDefaultQuestion.text = "<size=70>" + question.Def + "</size>";
        }
        else
        {
            textRubyQuestion.text = question.Ruby;
            textDefaultQuestion.text = question.Def;
        }
        textLatinAlphabetQuestion.text = question.Latin;
        showingLatinAlphabet = question.Latin;
    }

    public void SetLatinAlphabetQuestionText(string latin)
    {
        showingLatinAlphabet = latin;
    }

    public void ChangeTextColorCorrect(int latinIndex)
    {
        textLatinAlphabetQuestion.text =
            colorGreen
            + showingLatinAlphabet.Substring(0, latinIndex)
            + colorEnd
            + showingLatinAlphabet.Substring(latinIndex);
    }

    public void ChangeTextColorIncorrect(int latinIndex)
    {
        textLatinAlphabetQuestion.text =
            colorGreen
            + showingLatinAlphabet.Substring(0, latinIndex)
            + colorEnd
            + colorRed
            + showingLatinAlphabet.Substring(latinIndex, 1)
            + colorEnd
            + showingLatinAlphabet.Substring(latinIndex + 1);
    }
}
