using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    // Queue is a FIFO (First-in, First-out), first element added is first element removed
    private Queue<string> sentences;
    
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Read sentence by sentence and display it on dialogue box
    public void StartDialogue(Dialogue dialogue)
    {
        // Window "Animator" will make use of this value to display / hide the dialogue box
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;
        
        sentences.Clear();

        // Enqueue every sentence for them to be displayed later in DisplayNextSentence()
        foreach (string sentence in dialogue.Sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    
    public void DisplayNextSentence()
    {
        // If no more sentences to read, end dialogue
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        // Removes and returns the first element from the Queue
        string sentence = sentences.Dequeue();

        // Stop all coroutines in case the player skips dialogue before finishing
        StopAllCoroutines();

        // Start coroutine to display letter by letter
        StartCoroutine(TypeSentence(sentence));
    }

    // Creates a sequence of values one at a time
    // In this case, it reveals letters one by one
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";

        // Iterate through each letter in the sentence
        foreach (char letter in sentence.ToCharArray())
        {
            // Add letter by letter from the sentence
            dialogueText.text += letter;
                
            // Pauses coroutine for one frame for effects over time like revealing letters
            yield return null;
        }
    }
    
    // End dialogue by hiding the dialogue box
    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        Cursor.visible = false;
    }
}
