using UnityEngine;
using System.IO;

public class JSON_Reader : MonoBehaviour
{
    private static NPCList npcList;

    void Awake()
    {
        // Create JSON file path
        string jsonFileName = "Dialogues_NPCs.json";
        string jsonFilePath = Path.Combine(JSON_Writer.dialogueDirectory, jsonFileName);

        // Read JSON file
        string jsonContent = File.ReadAllText(jsonFilePath);
        
        // Deserialize JSON string into NPCList
        npcList = JsonUtility.FromJson<NPCList>(jsonContent);
    }

    // Return NPCData from npcList to their respective NPC
    public static NPCData GetNPCDataForDialogue(Dialogue dialogue)
    {
        string npcName = dialogue.name;

        foreach (NPCData npcData in npcList.NPCs)
        {
            if (npcData.name == npcName)
            {
                return npcData;
            }
        }

        return null;
    }
}