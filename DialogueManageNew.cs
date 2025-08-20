using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections.Generic;

public class DialogueManageNew : MonoBehaviour
{
    public Canvas dialogueCanvas;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    private Queue<string> inputStream = new Queue<string>();

    void Start()
    {
        dialogueCanvas.gameObject.SetActive(false);
    }

    public void StartDialogue(Queue<string> dialogue)
    {
        dialogueCanvas.gameObject.SetActive(true);
        inputStream = dialogue;
        PrintDialogue();
        print("DialogueStarted");
    }

    public void AdvanceDialogue()
    {
        PrintDialogue();
    }

    private void PrintDialogue()
    {
        if (inputStream.Peek().Contains("EndQueue"))
        {
            inputStream.Dequeue();
            EndDialogue();
        }
        else if (inputStream.Peek().Contains("[Name="))
        {
            string name = inputStream.Peek();
            name = inputStream.Dequeue().Substring(name.IndexOf('=') + 1, name.IndexOf(']') - (name.IndexOf('=') + 1));
            nameText.text = name;
            PrintDialogue();
        }

        print("Printing");
    }

    public void EndDialogue()
    {
        dialogueText.text = "";
        nameText.text = "";
        inputStream.Clear();
        dialogueCanvas.gameObject.SetActive(false);
        print("ending");    
    }
}
