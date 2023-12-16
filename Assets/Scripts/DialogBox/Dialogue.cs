using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;

    private string[] dialogueMaster;
    private int indexOrder;
    private string[] sentences;

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

    public void LoadDialogueMaster()
    {
        DialogueMaster = JSON_Reader.GetDialogueMaster(this);
    }

    public void LoadNextDialogueFile()
    {
        Sentences = JSON_Reader.LoadNextDialogueFile(this);
        IndexOrder += 1;
    }
}
