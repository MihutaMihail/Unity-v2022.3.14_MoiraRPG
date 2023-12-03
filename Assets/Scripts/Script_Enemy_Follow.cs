using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Enemy_Follow : MonoBehaviour
{
    // Public
    public float moveSpeed;
    public Color attackColor;

    // Private
    private Rigidbody2D rb;
    private Transform player;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private bool isFollowing = false;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isFollowing)
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;
        }       
    }
    
    private void FixedUpdate()
    {
        if (isFollowing)
        {
            FollowPlayer(movement);
        }
    }

    private void FollowPlayer(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    // Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isFollowing = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isFollowing = false;
    }
}
