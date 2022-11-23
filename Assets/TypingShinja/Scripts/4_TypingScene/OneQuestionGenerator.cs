using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class OneQuestionGenerator
{
    ShowingQuestion showingQuestion = new ShowingQuestion();
    LatinAlphabetGenerater latinAlphabetGenerater = new LatinAlphabetGenerater();
    Subject<ShowingQuestion> showingQuestionSubject = new Subject<ShowingQuestion>();

    public ShowingQuestion ShowingQuestion
    {
        get => showingQuestion;
    }
    public IObservable<ShowingQuestion> OnShowingQuestionGenerated
    {
        get => showingQuestionSubject;
    }

    public void GenerateShowingQuestion(Difficulty dif)
    {
        int indexOfQuestionData = UnityEngine.Random.Range(0, CurrentQuestions.Instance.Questions.Length);

        string ruby = CurrentQuestions.Instance.Questions[indexOfQuestionData].Ruby;
        string def = CurrentQuestions.Instance.Questions[indexOfQuestionData].Def;
        string latin;
        if (dif <= Difficulty.Beginner5 || dif == Difficulty.Intermediate5)
        {
            latin = CurrentQuestions.Instance.Questions[indexOfQuestionData].Def;
        }
        else
        {
            latin = latinAlphabetGenerater.GenerateLatinAlphabet(ruby);
        }
        showingQuestion.Ruby = ruby;
        showingQuestion.Def = def;
        showingQuestion.Latin = latin;
        showingQuestionSubject.OnNext(showingQuestion);
    }

    public void CompleteStreamSouce()
    {
        showingQuestionSubject.OnCompleted();
    }
}
