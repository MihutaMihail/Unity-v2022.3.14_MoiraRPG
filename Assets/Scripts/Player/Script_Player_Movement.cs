using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player_Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void PlayerMovement(Vector2 dir, float speed)
    {
        rb.velocity = GetDirection(dir) * speed;
    }

    public void PlayerDash(Vector2 dir, float speed) 
    {
        rb.velocity = GetDirection(dir) * (speed * 3);
    }

    private Vector2 GetDirection(Vector2 dir)
    {
        // Normalize vector to ensure consistent speed in all directions
        dir.Normalize();

        // Calculate direction
        dir = dir.x * transform.right + dir.y * transform.up;

        return dir;
    }
}
