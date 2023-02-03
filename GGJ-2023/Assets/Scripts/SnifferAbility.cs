using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SnifferAbility : MonoBehaviour
{

    [SerializeField] private List<DiggableItem> diggableItemsInRange = new List<DiggableItem>();

    [SerializeField] private DiggableItem closestDiggableItem;

    [SerializeField] private Slider rangeIndicator;
    [SerializeField] private Slider staminaIndicator;

    [SerializeField] private float stamina;

    private float distance;


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

        staminaIndicator.value = Mathf.MoveTowards(staminaIndicator.value, stamina, 0.4f * Time.deltaTime);

        if (closestDiggableItem)
        {
            rangeIndicator.maxValue = closestDiggableItem.radius;
            //rangeIndicator.value = Mathf.InverseLerp(0,
            //                                 closestDiggableItem.radius,
            //                                 Vector2.Distance(transform.position, closestDiggableItem.transform.position));
            //Mathf.Lerp(0,)
            distance = Vector2.Distance(closestDiggableItem.transform.position, transform.position);
            rangeIndicator.value = Mathf.InverseLerp(closestDiggableItem.radius, closestDiggableItem.innerRadius, distance) * 3;

        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(closestDiggableItem)
            {
                if (distance < closestDiggableItem.innerRadius)
                {
                    //start digging sequence
                }
            }
            stamina -= 1;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DiggableItem"))
        {
            Debug.Log("fakka");
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
