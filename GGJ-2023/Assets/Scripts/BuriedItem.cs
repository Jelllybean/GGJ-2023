using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuriedItem : MonoBehaviour
{
    public bool hasBeenDug;
    public PuzzlePiece correspondingPuzzlePiece;

    public void SetToFound()
    {
        hasBeenDug = true;
        correspondingPuzzlePiece.isFound = true;
    }
}
