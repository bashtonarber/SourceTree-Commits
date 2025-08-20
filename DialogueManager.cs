using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Rendering;

/*DIALOGUE MANAGER!
This script is a part of the Dialogue system that is being used in a Horror game that I am working on. 



*/

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI[] nameText;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueText2;
    public Canvas dialogueCanvas;
    public string[] lines;

    private int index;
    public float textSpeed;
    
    public Image[] nameBox;

    private string input;

    public Queue<string> sentences;

    public static bool dialogueOpened;
    public Button closeDialogueButton;

    void Start()
    {
        sentences = new Queue<string>();
        dialogueText.gameObject.SetActive(true);
        dialogueText2.gameObject.SetActive(false);
        dialogueCanvas.gameObject.SetActive(false);
    }

    void Update()
    {

    }

    #region IntroDialogue
    public void StartDialogue(DialogueScript dialogue)
    {
        dialogueCanvas.gameObject.SetActive(true);
        nameText[0].text = dialogue.name;
        nameText[1].text = dialogue.name2;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayFirstSentence();
        closeDialogueButton.gameObject.SetActive(false);

    }
    #endregion

    public void DisplayFirstSentence()
    {
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        nameBox[0].gameObject.SetActive(true);
        nameBox[1].gameObject.SetActive(false);
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        if (dialogueText.gameObject.activeInHierarchy)
        {
            dialogueText.gameObject.SetActive(false);
            dialogueText2.gameObject.SetActive(true);
            nameBox[0].gameObject.SetActive(true);
            nameBox[1].gameObject.SetActive(false);
        }
        else if (dialogueText2.gameObject.activeInHierarchy)
        {
            dialogueText2.gameObject.SetActive(false);
            dialogueText.gameObject.SetActive(true);
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        dialogueText2.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

        /*

        dialogueText[1].text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }*/
    }

    void EndDialogue()
    {
        print("DONE!!!!");
        closeDialogueButton.gameObject.SetActive(true);
    }

    public void CloseDialogueBox()
    {
        dialogueCanvas.gameObject.SetActive(false);
    }

    public void ReadStringInput(string s)
    {
        input = s;
        print(input);
    } 
}
    
    /* private string GetLineAtIndex(int index) // To add?
    {
        string[] lines = File.ReadAllLines(filePath);

        if (index < lines.Length)
        {
            return lines[index];
        }
        else
        {
            return "NO MORE LINES";
        }
    }*/


                
                /* void Start()
{
dialogueText.text = string.Empty;
dialogueOpened = false;
//   StartDialogue();
}

void Update()
{
playerCanvas.gameObject.SetActive(false);
dialogueCanvas.gameObject.SetActive(true);


}
/*
public void StartDialogue()
{
index = 0;
StartCoroutine(TypeLine());
}

IEnumerator TypeLine()
{
foreach (char letters in lines[index].ToCharArray())
{
dialogueText.text += letters;
yield return null; 
}
}

public void NextLine()
{
if (index < lines.Length - 1)
{
index++;
dialogueText.text = string.Empty;
StartCoroutine(TypeLine());
}
else
{
gameObject.SetActive(false);
}
}*/


