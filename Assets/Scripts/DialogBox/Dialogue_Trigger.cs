using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour
{
    public Dialogue dialogue;

    // This script will be added to any NPC that has something to say to the MainCharacter
    // It serves the purpose of starting the conversation
    public void TriggerDialogue()
    {
        // Setup
        Cursor.visible = true;

        // Load dialogue master
        dialogue.LoadDialogueMaster();  

        // Load next dialogue file
        dialogue.LoadNextDialogueFile();

        // Start Dialogue
        FindObjectOfType<Dialogue_Manager>().StartDialogue(dialogue);
    }
}
