using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor; // To add things like [MenuItem()]

public class JSON_Writer : MonoBehaviour
{   
    // Create custom inspector for unity
    [MenuItem("Custom/Generate NPC JSON File")]
    public static void GenerateNPCJSONFile()
    {
        NPCList npcList = new NPCList();
        npcList.NPCs = new List<NPCData>();

        // Populate list with NPCs
        npcList.NPCs.Add(new NPCData
        {
            name = "The Archer",
            folder = "NPC_Archer",
            dialoguesFiles = new string[] { "Dialogue_1", "Dialogue_2" }
        });

        npcList.NPCs.Add(new NPCData
        {
            name = "The Wizard",
            folder = "NPC_Wizard",
            dialoguesFiles = new string[] { "Dialogue_1", "Dialogue_2"}
        });

        // Serialize to JSON
        string json = JsonUtility.ToJson(npcList, true);

        // Save JSON file
        string pathInAssets = "Dialogues_Archive/Dialogues_NPCs.json";
        string fullPath = Path.Combine(Application.dataPath, pathInAssets);

        File.WriteAllText(fullPath, json);
    }
}
