using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerView : MonoBehaviour
{
    IFingerPiece[] interfaceOfFingerPieces;

    char previousKey;

    void Awake()
    {
        interfaceOfFingerPieces = GameObjectExtensions.FindObjectsOfInterface<IFingerPiece>();
    }

    public void ActiveFingerPieceOf(string nextKeyToInput)
    {
        char nextKey = nextKeyToInput[0];

        foreach (IFingerPiece iFingerPiece in interfaceOfFingerPieces)
        {
            for (int i = 0; i < iFingerPiece.AlphabetThatShouldReact.Count; i++)
            {
                if (nextKey == iFingerPiece.AlphabetThatShouldReact[i])
                {
                    iFingerPiece.Status = STATUS.Active;
                    previousKey = nextKey;
                    break;
                }
            }
        }
    }

    public void InactiveFingerPieceOfPrevious()
    {
        foreach (IFingerPiece iFingerPiece in interfaceOfFingerPieces)
        {
            for (int i = 0; i < iFingerPiece.AlphabetThatShouldReact.Count; i++)
            {
                if (previousKey == iFingerPiece.AlphabetThatShouldReact[i])
                {
                    iFingerPiece.Status = STATUS.Inactive;
                    break;
                }
            }
        }
    }

    
}
