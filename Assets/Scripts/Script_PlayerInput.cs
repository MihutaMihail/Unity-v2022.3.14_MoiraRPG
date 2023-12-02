using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PlayerInput : MonoBehaviour
{
    // Public
    public float PlayerSpeed;

    // Private
    private Script_PlayerMovement ScriptPlayerMovement;

    void Awake()
    {
        ScriptPlayerMovement = GetComponent<Script_PlayerMovement>();
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 dir = new Vector2(0, 0);
        
        if (Input.GetKey(KeyCode.RightArrow)) dir.x = 1; // Right
        if (Input.GetKey(KeyCode.LeftArrow)) dir.x = -1; // Left
        if (Input.GetKey(KeyCode.UpArrow)) dir.y = 1; // Up
        if (Input.GetKey(KeyCode.DownArrow)) dir.y = -1; // Down

        ScriptPlayerMovement.PlayerMovement(dir, PlayerSpeed);
    }
}