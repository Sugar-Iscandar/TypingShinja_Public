using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerPiece : MonoBehaviour, IFingerPiece
{
    STATUS status;

    public STATUS Status
    {
        get
        {
            return status;
        }
        set
        {
            status = value;
            ChangeFingerColor();
        }
    }

    [SerializeField] List<char> alphabetThatShouldReact = new List<char>();

    public List<char> AlphabetThatShouldReact
    {
        get
        {
            return alphabetThatShouldReact;
        }
    }

    void Awake()
    {
        status = STATUS.Inactive;
    }

    void ChangeFingerColor()
    {
        Image image = this.gameObject.GetComponent<Image>();

        switch(status)
        {
            case STATUS.Inactive:
                image.color = Color.white;
                break;
            case STATUS.Active:
                image.color = Color.green;
                break;
        }
    }
}
