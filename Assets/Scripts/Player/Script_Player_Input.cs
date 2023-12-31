using UnityEngine;

public class Script_Player_Input : MonoBehaviour
{
    public float moveSpeed;

    private Script_Player_Movement ScriptPlayerMovement;

    void Awake()
    {
        ScriptPlayerMovement = GetComponent<Script_Player_Movement>();
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 dir = new Vector2(0, 0);

        // QWERTY Controls
        if (Input.GetKey(KeyCode.D)) dir.x = 1; // Right
        if (Input.GetKey(KeyCode.A)) dir.x = -1; // Left
        if (Input.GetKey(KeyCode.W)) dir.y = 1; // Up
        if (Input.GetKey(KeyCode.S)) dir.y = -1; // Down

        // AZERTY Controls
        if (Input.GetKey(KeyCode.Q)) dir.x = -1; // Left
        if (Input.GetKey(KeyCode.Z)) dir.y = 1; // Up

        ScriptPlayerMovement.PlayerMovement(dir, moveSpeed);

        // Dash / Dodge
        if (Input.GetKey(KeyCode.LeftShift)) ScriptPlayerMovement.PlayerDash(dir, moveSpeed);
    }
}