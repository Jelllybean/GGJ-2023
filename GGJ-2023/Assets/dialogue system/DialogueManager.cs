using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public GameObject dialogueBox;
    public List<GameObject> objectsToTurnOff = new List<GameObject>();
    public List<GameObject> objectsToTurnOn = new List<GameObject>();
    private Queue<string> sentences;
    
    void Awake()
    {
        sentences = new Queue<string>();
    }

   public void StartDialogue (Dialogue dialogue)
    {
        dialogueBox.SetActive(true);

        nameText.text= dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }
    
    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        dialogueBox.SetActive(false);
        for (int i = 0; i < objectsToTurnOff.Count; i++)
        {
            objectsToTurnOff[i].SetActive(false);
        }
        for (int i = 0; i < objectsToTurnOn.Count; i++)
        {
            objectsToTurnOn[i].SetActive(true);
        }
    }
}
