using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyView : MonoBehaviour
{
    IKeyPiece[] interfaceOfKeyPieces;

    char previousKey;

    void Awake()
    {
        interfaceOfKeyPieces = GameObjectExtensions.FindObjectsOfInterface<IKeyPiece>();
    }

    public void ActiveKeyPieceOf(string nextKeyToInput)
    {
        char nextKey = nextKeyToInput[0];

        foreach (IKeyPiece iKeyPiece in interfaceOfKeyPieces)
        {
            for (int i = 0; i < iKeyPiece.KEY_GETTER.Count; i++)
            {
                if (nextKey == iKeyPiece.KEY_GETTER[i])
                {
                    iKeyPiece.Status = STATUS.Active;
                    previousKey = nextKey;
                    break;
                }
            }
        }
    }

    public void InActiveKeyPieceOfPrevious()
    {
        foreach (IKeyPiece iKeyPiece in interfaceOfKeyPieces)
        {
            for (int i = 0; i < iKeyPiece.KEY_GETTER.Count; i++)
            {
                if (previousKey == iKeyPiece.KEY_GETTER[i])
                {
                    iKeyPiece.Status = STATUS.Inactive;
                    break;
                }
            }
        }
    }
}
