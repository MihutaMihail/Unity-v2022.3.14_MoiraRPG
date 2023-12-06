using UnityEngine;
using UnityEditor;

public class JSON_WriterEditor : Editor
{
    // Specify which file the custom inspector will edit
    [CustomEditor(typeof(JSON_Writer))]
    public override void OnInspectorGUI()
    {
        // Draw the default Inspector Layout provided by Unity
        base.OnInspectorGUI();

        // Check if the inspector button pressed by the user is the one we want
        if (GUILayout.Button("Generate NPC JSON File"))
        {
            // The assets hierarchy needs to be refreshed so that the json file is displayed
            // This can be done as simply as going to visual studio and immediately returning to unity
            JSON_Writer.GenerateNPCJSONFile();
        }
    }
}
