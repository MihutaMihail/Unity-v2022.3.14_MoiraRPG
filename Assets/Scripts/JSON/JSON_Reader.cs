using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSON_Reader : MonoBehaviour
{
    void Awake()
    {
        // Get JSON file path
        string pathInAssets = "Dialogues_Archive/Dialogues_NPCs.json";
        string fullPath = Path.Combine(Application.dataPath, pathInAssets);

        // Read JSON file
        string jsonContent = File.ReadAllText(fullPath);

        // Deserialize JSON string into NPCList
        NPCList npcList = JsonUtility.FromJson<NPCList>(jsonContent);

        // Show all NPCs properties (TEST)
        /*foreach (NPCData npcData in npcList.NPCs)
        {
            Debug.Log($"Name: {npcData.name}, Folder: {npcData.directory}");
            Debug.Log($"Text Files: {string.Join(", ", npcData.dialoguesFiles)}");
        }*/
    }
}