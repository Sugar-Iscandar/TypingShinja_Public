using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class KeyInputObserver : MonoBehaviour
{
    KeyCode keyCodeJustEnterd;
    string keyJustEntered;
    bool isShiftEnterd;
    //public event UnityAction<string> OnAnyKeyEntered = null;
    Subject<string> keyJustEnterSubject = new Subject<string>();

    public IObservable<string> OnAnyKeyEntered
    {
        get => keyJustEnterSubject;
    }

    // Update is called once per frame
    void Update()
    {
        isShiftEnterd = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    keyCodeJustEnterd = keyCode;
                    break;
                }
            }
            keyJustEntered = keyCodeJustEnterd.ToString().ToLower();
            //記号の入力だった場合、記号の文字列に書き換えます
            SymbolCheck();
            Debug.Log("今押されたキーは" + keyJustEntered);
            keyJustEnterSubject.OnNext(keyJustEntered);
        }
    }

    public void SymbolCheck()
    {
        switch (keyCodeJustEnterd)
        {
            case KeyCode.Alpha1:
                keyJustEntered = "1";
                if (isShiftEnterd)
                    keyJustEntered = "!";
                break;
            case KeyCode.Alpha2:
                keyJustEntered = "2";
                if (isShiftEnterd)
                    keyJustEntered = "\"";
                break;
            case KeyCode.Alpha3:
                keyJustEntered = "3";
                if (isShiftEnterd)
                    keyJustEntered = "#";
                break;
            case KeyCode.Alpha4:
                keyJustEntered = "4";
                if (isShiftEnterd)
                    keyJustEntered = "$";
                break;
            case KeyCode.Alpha5:
                keyJustEntered = "5";
                if (isShiftEnterd)
                    keyJustEntered = "%";
                break;
            case KeyCode.Alpha6:
                keyJustEntered = "6";
                if (isShiftEnterd)
                    keyJustEntered = "&";
                break;
            case KeyCode.Alpha7:
                keyJustEntered = "7";
                if (isShiftEnterd)
                    keyJustEntered = "'";
                break;
            case KeyCode.Alpha8:
                keyJustEntered = "8";
                if (isShiftEnterd)
                    keyJustEntered = "(";
                break;
            case KeyCode.Alpha9:
                keyJustEntered = "9";
                if (isShiftEnterd)
                    keyJustEntered = ")";
                break;
            case KeyCode.Minus:
                keyJustEntered = "-";
                if (isShiftEnterd)
                    keyJustEntered = "=";
                break;
            case KeyCode.At:
                keyJustEntered = "@";
                if (isShiftEnterd)
                    keyJustEntered = "`";
                break;
            case KeyCode.LeftBracket:
                keyJustEntered = "[";
                if (isShiftEnterd)
                    keyJustEntered = "{";
                break;
            case KeyCode.RightBracket:
                keyJustEntered = "]";
                if (isShiftEnterd)
                    keyJustEntered = "}";
                break;
            case KeyCode.Semicolon:
                keyJustEntered = ";";
                if (isShiftEnterd)
                    keyJustEntered = "+";
                break;
            case KeyCode.Colon:
                keyJustEntered = ":";
                if (isShiftEnterd)
                    keyJustEntered = "*";
                break;
            case KeyCode.Slash:
                keyJustEntered = "/";
                if (isShiftEnterd)
                    keyJustEntered = "?";
                break;
            case KeyCode.Underscore:
                keyJustEntered = "_";
                break;
        }
    }

    public void CompleteStreamSouce()
    {
        keyJustEnterSubject.OnCompleted();
    }
}
