using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour
{
    public Dialogue dialogue;

    void Start()
    {
        dialogue.GetNPCData();
        dialogue.LoadDialogueMaster();
    }

    public void TriggerDialogue()
    {
        Cursor.visible = true;

        // Load next dialogue file
        dialogue.LoadNextDialogueFile();
        
        // Start Dialogue
        FindObjectOfType<Dialogue_Manager>().StartDialogue(dialogue);
    }
}
