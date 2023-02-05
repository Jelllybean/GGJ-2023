using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfComplete : MonoBehaviour
{
    [SerializeField] private List<PuzzlePiece> puzzlePieces = new List<PuzzlePiece>();
    [SerializeField] private Transform completeText;

    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            if(IsAllMissionComplete(puzzlePieces[i].correctlyPlaced))
            {
                completeText.gameObject.SetActive(true);
                completeText.position = new Vector3(puzzlePieces[i].transform.position.x, puzzlePieces[i].transform.position.y, completeText.position.z);
                Invoke("RemoveItem", 3f);
                break;
            }
        }
    }

    private bool IsAllMissionComplete(List<bool> _list)
    {
        for (int i = 0; i < _list.Count; ++i)
        {
            if (_list[i] == false)
            {
                return false;
            }
        }

        return true;
    }

    public void RemoveItem()
    {

        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            puzzlePieces[i].gameObject.SetActive(false);
        }
    }
}
