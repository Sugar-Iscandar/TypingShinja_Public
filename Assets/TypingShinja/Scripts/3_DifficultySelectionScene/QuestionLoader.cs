using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestionLoader
{
    public Question[] LoadQuestion(Difficulty difficulty)
    {
        string fileName = "QuestionData_" + ((int)difficulty).ToString();
        Debugger.Log("読み出すファイル：" + fileName);
        List<Question> questions = new List<Question>();
        TextAsset questionCSV = Resources.Load("QuestionData/" + fileName) as TextAsset;
        StringReader reader = new StringReader(questionCSV.text);
        //1行目は無視
        reader.ReadLine();
        while (reader.Peek() > -1) 
        {
            string line = reader.ReadLine();
            string[] rubyAndDefault = line.Split(',');
            Question question = new Question();
            question.Ruby = rubyAndDefault[0];
            question.Def = rubyAndDefault[1];
            questions.Add(question);
        }
        return questions.ToArray();
    }
}
