using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Enemy_Follow : MonoBehaviour
{
    // Public
    public float moveSpeed;
    public Color attackColor;

    // Private
    private const float ReturnThreshold = 0.1f;

    private Rigidbody2D rigidBody;
    private Vector3 startPosition;
    private Transform player;

    private Vector2 movement;
    private Vector2 returnMovement;
    private bool isFollowing = false;
    private bool isReturningToStartingPosition = false;

    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        startPosition = transform.position;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (isFollowing) movement = CalculateDirection(player.position);
        else if (isReturningToStartingPosition) returnMovement = CalculateDirection(startPosition);
    }

    private void FixedUpdate()
    {
        if (isFollowing) FollowPlayer(movement);
        else if (isReturningToStartingPosition) ReturnToStartingPosition(returnMovement);
    }

    //
    // Direction
    //

    private Vector2 CalculateDirection(Vector3 targetPosition)
    {
        // Calculate vector pointing from the enemy position to the player position
        Vector2 direction = targetPosition - transform.position;

        // Normalize vector to get direction from enemy to player
        direction.Normalize();

        return direction;
    }

    //
    // Movement
    //

    private void FollowPlayer(Vector2 direction)
    {
        // Move gameObject(enemy) towards player
        rigidBody.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
    }

    private void ReturnToStartingPosition(Vector2 returnDirection)
    {
        // Move gameObject(enemy) towards starting position
        rigidBody.MovePosition((Vector2)transform.position + (returnDirection * moveSpeed * Time.fixedDeltaTime));

        // Calculate distance between gameObject and player
        float distanceToStartingPosition = Vector2.Distance(transform.position, startPosition);

        // Check if distance to starting position from the enemy position is in threshold
        if (distanceToStartingPosition < ReturnThreshold)
        {
            // Stop further movement
            rigidBody.velocity = Vector2.zero;
            
            // Reset enemy to starting position
            transform.position = startPosition;
            
            // Reset flag
            isReturningToStartingPosition = false;
        }
    }

    //
    // Trigger
    //

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isFollowing = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFollowing = false;
            isReturningToStartingPosition = true;
        }
    }
}