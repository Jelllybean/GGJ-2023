using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueManager manager;
    public Dialogue dialogue;

    private void Start()
    {
        manager.StartDialogue(dialogue);
        gameObject.SetActive(false);
    }

    public void TriggerDialogue()
    {
        
    }
}
