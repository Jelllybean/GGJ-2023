using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void Start()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        gameObject.SetActive(false);
    }

    public void TriggerDialogue()
    {
        
    }
}