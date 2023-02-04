using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public enum ItemType { Vase, Boar1, Boar2, StonePlaque, Statue };

    public ItemType itemType;

    public int pieceIndex = 0;
    public bool isFound = false;

    [SerializeField] private List<Vector2> raycastDirections = new List<Vector2>();
    [SerializeField] private List<int> requiredIndex = new List<int>();
    [SerializeField] private List<bool> correctlyPlaced = new List<bool>();
    [SerializeField] private LayerMask puzzleLayer;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Placed()
    {
        for (int i = 0; i < raycastDirections.Count; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirections[i], 1, puzzleLayer);
            if(hit.collider)
            {
                Debug.Log(hit.collider.name);
                PuzzlePiece _hitPiece = hit.collider.GetComponent<PuzzlePiece>();
                if(_hitPiece.itemType == itemType && _hitPiece.pieceIndex == requiredIndex[i])
                {
                    correctlyPlaced[i] = true;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < raycastDirections.Count; i++)
        {
            Gizmos.DrawRay(transform.position, raycastDirections[i]);
        }
    }
}
