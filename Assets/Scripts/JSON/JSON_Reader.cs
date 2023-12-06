using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSON_Reader : MonoBehaviour
{
    public TextAsset dialoguesJson;

    void Start()
    {
        // Read JSON file
        string pathInAssets = "Dialogues_Archive/Dialogues_NPCs.json";
        string fullPath = Path.Combine(Application.dataPath, pathInAssets);
        
        string jsonString = File.ReadAllText(fullPath);

        // TO DO
        // JSON data is in jsonString
        // store the json data inside the NPC data
    }
}
