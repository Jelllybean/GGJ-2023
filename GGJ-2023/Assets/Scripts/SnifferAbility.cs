using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SnifferAbility : MonoBehaviour
{

    [SerializeField] private List<DiggableItem> diggableItemsInRange = new List<DiggableItem>();

    [SerializeField] private DiggableItem closestDiggableItem;
    [SerializeField] private GameObject miniGameHolder;
    [SerializeField] private GameObject normalGameHolder;
    [SerializeField] private GameObject puzzleGameHolder;

    [SerializeField] private Slider rangeIndicator;
    [SerializeField] private Slider staminaIndicator;

    public float stamina;

    private float distance;

    [Header("Audio")]
    [SerializeField] private AudioSource oinkNoisePlayer;
    [SerializeField] private List<AudioClip> oinkNoises = new List<AudioClip>();

    private void Start()
    {
        staminaIndicator.maxValue = stamina;
        staminaIndicator.value = stamina;
    }

    private void Update()
    {
        float _lowestDistance = 1000;
        for (int i = 0; i < diggableItemsInRange.Count; i++)
        {
            float _distance = Vector2.Distance(transform.position, diggableItemsInRange[i].transform.position);
            if(_distance < _lowestDistance)
            {
                _lowestDistance= _distance;
                closestDiggableItem = diggableItemsInRange[i];
            }
        }

        staminaIndicator.value = Mathf.MoveTowards(staminaIndicator.value, stamina, 2f * Time.deltaTime);

        if (closestDiggableItem)
        {
            rangeIndicator.maxValue = closestDiggableItem.radius;
            //rangeIndicator.value = Mathf.InverseLerp(0,
            //                                 closestDiggableItem.radius,
            //                                 Vector2.Distance(transform.position, closestDiggableItem.transform.position));
            //Mathf.Lerp(0,)
            distance = Vector2.Distance(closestDiggableItem.transform.position, transform.position);
            rangeIndicator.value = Mathf.InverseLerp(closestDiggableItem.radius, closestDiggableItem.innerRadius, distance) * 3;
            if(distance < closestDiggableItem.innerRadius)
            {
                rangeIndicator.value = rangeIndicator.maxValue;
            }
            if(!oinkNoisePlayer.isPlaying)
            {
                oinkNoisePlayer.clip = oinkNoises[Random.Range(0, oinkNoises.Count)];
                oinkNoisePlayer.Play();
            }
        }


        if (Input.GetKeyDown(KeyCode.Space) && Movement.MovementSingleton.canMove)
        {
            if(closestDiggableItem)
            {
                if (distance < closestDiggableItem.innerRadius)
                {
                    //start digging sequence
                    StartDigging();
                }
            }
            stamina -= 1;
        }

        if(stamina <= 0)
        {
            miniGameHolder.SetActive(false);
            normalGameHolder.SetActive(false);
            puzzleGameHolder.SetActive(true);
            puzzleGameHolder.GetComponent<TruckScene>().enabled = true;
            gameObject.SetActive(false);
        }
    }

    public void StartDigging()
    {
        miniGameHolder.SetActive(true);
        normalGameHolder.SetActive(false);
        Movement.MovementSingleton.canMove = false;
        closestDiggableItem.gameObject.SetActive(false);
        Camera.main.gameObject.SetActive(false);
        SpawnRandomDiggableItem.spawnRandomDiggableItem.SpawnRandomPlace();
        closestDiggableItem = null;
        rangeIndicator.value = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DiggableItem"))
        {
            diggableItemsInRange.Add(collision.GetComponent<DiggableItem>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DiggableItem"))
        {
            diggableItemsInRange.Remove(collision.GetComponent<DiggableItem>());
        }
    }
}
