using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentQuestions
{
    static CurrentQuestions instance = new CurrentQuestions();

    public static CurrentQuestions Instance
    {
        get => instance;
    }

    private CurrentQuestions() { }

    Question[] questions;

    public Question[] Questions
    {
        get => questions;
        set => questions = value;
    }
}
