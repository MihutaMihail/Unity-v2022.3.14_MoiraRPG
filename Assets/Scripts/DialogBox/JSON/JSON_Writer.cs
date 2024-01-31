using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor; // To add things like [MenuItem()]
using System;

public class JSON_Writer : MonoBehaviour
{
    public static string dialogueDirectory = Path.Combine(Application.dataPath, "Dialogues_Archive/");
    public static string dialogueMasterName = "DialogueMaster";

    // Create custom inspector for Unity
    [MenuItem("Custom/Generate NPC JSON File")]
    public static void GenerateNPCJSONFile()
    {
        NPCList npcList = new NPCList();
        npcList.NPCs = new List<NPCData>();

        // Populate list with NPCs
        npcList.NPCs.Add(new NPCData
        {
            name = "Bob",
            directory = "NPC_Archer/",
            dialogueMaster = dialogueMasterName,
            dialoguesFiles = new string[] { "Quest1_Start", "Quest1_End" }
        });

        npcList.NPCs.Add(new NPCData
        {
            name = "Mario",
            directory = "NPC_Wizard/",
            dialogueMaster = dialogueMasterName,
            dialoguesFiles = new string[] { "Quest2_Start", "Quest2_End", "Quest2_End2" }
        });

        // Serialize to JSON
        string json = JsonUtility.ToJson(npcList, true);

        // JSON file saving path
        string jsonFileName = "Dialogues_NPCs.json";
        string fullPath = Path.Combine(dialogueDirectory, jsonFileName);
        
        // Write JSON file
        File.WriteAllText(fullPath, json);

        GenerateDirectory(npcList);
    }

    // Create directory / files for each NPC
    public static void GenerateDirectory(NPCList npcList)
    {
        foreach (NPCData npcData in npcList.NPCs)
        {
            // Create directory path
            string npcDirPath = Path.Combine(dialogueDirectory, npcData.directory);

            // Create directory
            if (!Directory.Exists(npcDirPath))
            {    
                Directory.CreateDirectory(npcDirPath);
            }
            
            // This is where we'll stock every dialogue file name in order
            List<string> dialogueMasterList = new List<string>();
            
            // Create dialogue files inside directory
            foreach (string dialogueFile in npcData.dialoguesFiles)
            {
                // Create dialogue file path
                string dialogueFilePath = Path.Combine(npcDirPath, dialogueFile + ".txt");
                
                // Create empty file
                if (!File.Exists(dialogueFilePath))
                {
                    File.WriteAllText(dialogueFilePath, "");
                }
                
                // Add file to dialogue master
                dialogueMasterList.Add(dialogueFile);
            }

            // Create dialogue master path
            string dialogueMasterPath = Path.Combine(npcDirPath, "DialogueMaster" + ".txt");
            
            // Join the list items into a single string
            string dialogueMasterListString = string.Join(Environment.NewLine, dialogueMasterList);

            // Create dialogue master file
            File.WriteAllText(dialogueMasterPath, dialogueMasterListString);
        }
    }
}
