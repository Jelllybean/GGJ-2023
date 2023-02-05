using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfComplete : MonoBehaviour
{
    [SerializeField] private List<PuzzlePiece> puzzlePieces = new List<PuzzlePiece>();
    [SerializeField] private Transform completeText;
    [SerializeField] private GameObject unlockCorrespondingMuseumPiece;

    public bool hasBeenCompleted = false;

    void Update()
    {
        if (!hasBeenCompleted)
        {
            for (int i = 0; i < puzzlePieces.Count; i++)
            {
                if (IsAllMissionComplete(puzzlePieces[i].correctlyPlaced))
                {
                    completeText.gameObject.SetActive(true);
                    //completeText.position = new Vector3(puzzlePieces[i].transform.position.x, puzzlePieces[i].transform.position.y, completeText.position.z);
                    Invoke("RemoveItem", 3f);
                    hasBeenCompleted = true;
                    break;
                }
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
        completeText.gameObject.SetActive(false);
        unlockCorrespondingMuseumPiece.SetActive(true);
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            puzzlePieces[i].gameObject.SetActive(false);
        }
        StoreScore.storeScoreInstance.AddScore(12500);
        gameObject.SetActive(false);
    }
}
