using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLaying : MonoBehaviour
{
    [SerializeField] private List<Transform> puzzleGridLocations = new List<Transform>();

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
            placementAudio.clip = audioClips[0];
            placementAudio.Play();
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
            for (int i = 0; i < puzzleGridLocations.Count; i++)
            {
                float _distance = Vector2.Distance(selectedPuzzlePiece.position, puzzleGridLocations[i].transform.position);
                if (_distance < _lowestDistance)
                {
                    _lowestDistance = _distance;
                    _closestLocation = puzzleGridLocations[i];
                }
            }
            selectedPuzzlePiece.position = _closestLocation.position;
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
}
