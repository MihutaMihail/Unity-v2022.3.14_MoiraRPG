using UnityEngine;

public class Enemy_Follow : MonoBehaviour
{
    public Vector2 CalculateDirection(Vector3 targetPosition)
    {
        // Vector pointing from the enemy position to the target position
        Vector2 direction = targetPosition - transform.position;

        direction.Normalize(); 

        return direction;
    }
    
    public void MoveToPosition(Vector2 direction, Rigidbody2D rb, float speed)
    {
        rb.MovePosition(
            (Vector2)transform.position + 
            (direction * speed * Time.fixedDeltaTime)
        );
    }
    
    public void ReturnToStartingPosition(Vector2 startPosition, Rigidbody2D rb, float speed, float returnThreshold, ref bool isReturningToStartingPosition)
    {
        MoveToPosition(startPosition, rb, speed);
        
        float distanceToStartingPosition = Vector2.Distance(transform.position, startPosition);
        
        if (distanceToStartingPosition < returnThreshold)
        {
            // Stop further movement
            rb.velocity = Vector2.zero;
             
            // Reset enemy to starting position
            transform.position = startPosition;

            // Reset flag
            isReturningToStartingPosition = false;
        }
    }
}