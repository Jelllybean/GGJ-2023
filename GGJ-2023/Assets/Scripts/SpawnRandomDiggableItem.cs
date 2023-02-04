using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomDiggableItem : MonoBehaviour
{
    [SerializeField] private List<BuriedItem> diggableItems = new List<BuriedItem>();
    [SerializeField] private CheckTransparency checkIfFound;

    [SerializeField] private Vector2 boundsStart;
    [SerializeField] private Vector2 boundsEnd;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            SpawnRandomPlace();
        }
    }
    public void SpawnRandomPlace()
    {
        int _random = Random.Range(0, diggableItems.Count);
        if (diggableItems[_random].hasBeenDug)
        {
            SpawnRandomPlace();
        }
        else
        {
            diggableItems[_random].transform.position = new Vector2((int)Random.Range(boundsStart.x, boundsEnd.x), (int)Random.Range(boundsStart.y, boundsEnd.y));
            checkIfFound.currentDugItem = diggableItems[_random];
            checkIfFound.buriedItem = diggableItems[_random].transform;
            checkIfFound.spriteRenderer = diggableItems[_random].GetComponent<SpriteRenderer>();
        }
    }
}
