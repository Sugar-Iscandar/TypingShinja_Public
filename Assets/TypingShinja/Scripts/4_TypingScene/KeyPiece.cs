using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum STATUS
{
    Inactive,
    Active
}

public class KeyPiece : MonoBehaviour, IKeyPiece
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
            ChangeKeyColor();
        }
    }

    [SerializeField] List<char> keys = new List<char>();

    public List<char> KEY_GETTER
    {
        get
        {
            return keys;
        }
    }

    void Awake()
    {
        Status = STATUS.Inactive;
    }

    void ChangeKeyColor()
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
