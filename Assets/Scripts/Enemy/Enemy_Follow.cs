using UnityEngine;

public class Enemy_Follow : MonoBehaviour
{
    // Calculate the direction of an object in reference to the GameObject
    public Vector2 CalculateDirection(Vector3 targetPosition)
    {
        // Vector pointing from the enemy position to the target position
        Vector2 direction = targetPosition - transform.position;

        direction.Normalize(); 

        return direction;
    }
    
    // Move GameObject towards the specified position
    public void MoveToPosition(Vector2 direction, Rigidbody2D rb, float speed)
    {
        rb.MovePosition(
            (Vector2)transform.position + 
            (direction * speed * Time.fixedDeltaTime)
        );
    }
    
    // Return GameObject to inital starting position
    public bool ReturnToStartingPosition(Vector2 startPosition, Rigidbody2D rb, float speed)
    {
        Vector2 startDirection = CalculateDirection(startPosition);
        MoveToPosition(startDirection, rb, speed);
        
        float distanceToStartingPosition = Vector2.Distance(startPosition, transform.position);

        if (distanceToStartingPosition < 0.1f)
        {
            // Stop further movement
            rb.velocity = Vector2.zero;
             
            // Reset enemy to starting position
            transform.position = startPosition;

            // Returning false will indicate to the enemy controller that the enemy has stopped moving
            // and is not returning to its starting position anymore
            return false;
        }

        // Returning true to keep the enemy advancing to its starting position
        return true;
    }
}