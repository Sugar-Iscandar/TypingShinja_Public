using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFingerPiece
{
    public STATUS Status { get; set; }

    public List<char> AlphabetThatShouldReact { get; }
}
