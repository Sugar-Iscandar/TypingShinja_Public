using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKeyPiece
{
    public STATUS Status { get; set; }

    public List<char> KEY_GETTER { get; }
}
