using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLaying : MonoBehaviour
{
    [SerializeField] private List<Transform> puzzleGridLocations = new List<Transform>();
    [SerializeField] private List<bool> isGridLocationTaken = new List<bool>();

    [SerializeField] private List<PuzzlePiece> puzzlePieces = new List<PuzzlePiece>();

    [SerializeField] private Transform selectedPuzzlePiece;
    [SerializeField] private LayerMask puzzlePieceLayer;

    [SerializeField] private AudioSource placementAudio;
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Input.mousePosition, Mathf.Infinity, puzzlePieceLayer);

            if (hit.collider != null)
            {
                selectedPuzzlePiece = hit.collider.transform;
            }
            if (selectedPuzzlePiece)
            {
                selectedPuzzlePiece.GetComponent<BoxCollider2D>().enabled = false;
                placementAudio.clip = audioClips[0];
                placementAudio.Play();

                float _lowestDistance = 1000;
                for (int i = 0; i < puzzleGridLocations.Count; i++)
                {
                    float _distance = Vector2.Distance(selectedPuzzlePiece.position, puzzleGridLocations[i].transform.position);
                    if (_distance < _lowestDistance)
                    {
                        _lowestDistance = _distance;
                        isGridLocationTaken[i] = false;
                    }
                }
            }

        }

        if (Input.GetMouseButton(0) && selectedPuzzlePiece)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            selectedPuzzlePiece.transform.position = mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && selectedPuzzlePiece)
        {
            float _lowestDistance = 1000;
            Transform _closestLocation = null;
            int _index = 0;
            for (int i = 0; i < puzzleGridLocations.Count; i++)
            {
                float _distance = Vector2.Distance(selectedPuzzlePiece.position, puzzleGridLocations[i].transform.position);
                if (_distance < _lowestDistance)
                {
                    _lowestDistance = _distance;
                    _closestLocation = puzzleGridLocations[i];
                    _index = i;
                }
            }
            selectedPuzzlePiece.GetComponent<BoxCollider2D>().enabled = true;

            isGridLocationTaken[_index] = true;
            selectedPuzzlePiece.position = _closestLocation.position;
            //StartCoroutine(CheckAllPieces());
            CheckAllPieces();
            placementAudio.clip = audioClips[1];
            placementAudio.Play();
        }

        //float _lowestDistance = 1000;
        //for (int i = 0; i < diggableItemsInRange.Count; i++)
        //{
        //    float _distance = Vector2.Distance(transform.position, diggableItemsInRange[i].transform.position);
        //    if (_distance < _lowestDistance)
        //    {
        //        _lowestDistance = _distance;
        //        closestDiggableItem = diggableItemsInRange[i];
        //    }
        //}
    }

    public void CheckAllPieces()
    {
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            //puzzlePieces[i].GetComponent<BoxCollider2D>().enabled = false;
            puzzlePieces[i].Placed();
            //puzzlePieces[i].GetComponent<BoxCollider2D>().enabled = true;
            //yield return new WaitForSeconds(5f);
        }
    }
}
