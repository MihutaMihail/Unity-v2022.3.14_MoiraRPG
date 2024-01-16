using System.IO;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    
    private NPCData npcData;
    private string[] dialogueMaster;
    private int indexOrder;
    private string[] sentences;

    public NPCData NPCData
    {
        get { return this.npcData; }
        set { this.npcData = value; }
    }

    public string[] DialogueMaster
    {
        get { return this.dialogueMaster; }
        set { this.dialogueMaster = value; }
    }
    
    public int IndexOrder
    {
        get { return this.indexOrder; }
        set { this.indexOrder = value; }
    }

    public string[] Sentences
    {
        get { return this.sentences; }
        set { this.sentences = value; }
    }

    public void GetNPCData()
    {
        NPCData = JSON_Reader.GetNPCDataForDialogue(this);
    }
    
    public void LoadDialogueMaster()
    {
        // Create NPC directory path
        string npcDirPath = Path.Combine(JSON_Writer.dialogueDirectory, NPCData.directory);

        // Create dialogue master file path
        string dialogueMasterPath = Path.Combine(npcDirPath, JSON_Writer.dialogueMasterName + ".txt");

        // Read the content of the dialogue master file
        if (File.Exists(dialogueMasterPath))
        {
            DialogueMaster = File.ReadAllLines(dialogueMasterPath);
        }
    }
    public void LoadNextDialogueFile()
    {
        // Create NPC directory path
        string npcDirPath = Path.Combine(JSON_Writer.dialogueDirectory, NPCData.directory);
        
        // Create next dialogue file path
        string dialogueMasterNextFile = dialogueMaster[IndexOrder];
        string nextDialogueFilePath = Path.Combine(npcDirPath, dialogueMasterNextFile + ".txt");

        if (File.Exists(nextDialogueFilePath))
        {
            Sentences = File.ReadAllLines(nextDialogueFilePath);
        }
        
        // Increase index to get next file
        if (IndexOrder < DialogueMaster.Length - 1)
        {
            IndexOrder += 1;
        }
    }
}