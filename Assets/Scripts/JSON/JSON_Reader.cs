using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSON_Reader : MonoBehaviour
{
    private static NPCList npcList;

    void Awake()
    {
        // Get JSON file path
        string pathInAssets = "Dialogues_Archive/Dialogues_NPCs.json";
        string fullPath = Path.Combine(Application.dataPath, pathInAssets);

        // Read JSON file
        string jsonContent = File.ReadAllText(fullPath);

        // Deserialize JSON string into NPCList
        npcList = JsonUtility.FromJson<NPCList>(jsonContent);

        // Show all NPCs properties (TEST)
        /*foreach (NPCData npcData in npcList.NPCs)
        {
            Debug.Log($"Name: {npcData.name}, Folder: {npcData.directory}");
            Debug.Log($"Text Files: {string.Join(", ", npcData.dialoguesFiles)}");
        }*/
    }

    public static string[] GetDialogueMaster(Dialogue dialogue)
    {
        string npcName = dialogue.name;
        string[] dialogueMasterContent = { };

        foreach (NPCData npcData in npcList.NPCs)
        {
            if (npcData.name == npcName)
            {
                string npcDirPath = Path.Combine(JSON_Writer.dialogueDirectory, npcData.directory);
                
                // Create dialogue master file path
                string dialogueMasterPath = Path.Combine(npcDirPath, JSON_Writer.dialogueMaster + ".txt");

                // Read the content of the dialogue master file
                if (File.Exists(dialogueMasterPath))
                {
                    dialogueMasterContent = File.ReadAllLines(dialogueMasterPath);

                    return dialogueMasterContent;
                }
            }
        }

        return null;
    }

    public static string[] LoadNextDialogueFile(Dialogue dialogue)
    {
        string npcName = dialogue.name;
        string[] dialogueFileContent = { };

        foreach (NPCData npcData in npcList.NPCs)
        {
            if (npcData.name == npcName)
            {
                string[] dialogueMaster = dialogue.DialogueMaster;

                string dialogueMasterFile = dialogueMaster[dialogue.IndexOrder];

                string npcDirPath = Path.Combine(JSON_Writer.dialogueDirectory, npcData.directory);

                string dialogueFileContentPath = Path.Combine(npcDirPath, dialogueMasterFile + ".txt");

                // Read the content of the dialogue master file
                if (File.Exists(dialogueFileContentPath))
                {
                    dialogueFileContent = File.ReadAllLines(dialogueFileContentPath);

                    return dialogueFileContent;
                }
            }
        }

        return null;
    }
}