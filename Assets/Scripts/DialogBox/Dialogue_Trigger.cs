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

        // (TEMP) this should be put somewhere in the quests / gameManager
        // (TEMP) should be executed whenever a quest advances / etc
        dialogue.LoadNextDialogueFile();

        // Start Dialogue
        FindObjectOfType<Dialogue_Manager>().StartDialogue(dialogue);
    }
}
