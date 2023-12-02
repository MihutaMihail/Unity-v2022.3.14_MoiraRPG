using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PlayerMovement : MonoBehaviour
{
    // Private
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void PlayerMovement(Vector2 dir, float speed)
    {
        // Normalize vector to ensure consistent speed in all directions
        dir.Normalize();
        dir = dir.x * transform.right + dir.y * transform.up;
        rb.velocity = dir * speed;
    }
}
