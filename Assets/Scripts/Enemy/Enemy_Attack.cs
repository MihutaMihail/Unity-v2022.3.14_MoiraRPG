using System.Collections;

using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    // Declare a delegate for the event
    public delegate void AttackCompleteEventHandler();

    // Declare the event using the delegate
    public event AttackCompleteEventHandler OnAttackComplete;

    public Vector2 playerDirection { get; set; }

    public void AttackPlayer()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        float elapsedTime = 0f;
        float rotationDuration = 1f;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -90f);

        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Apply velocity for the attack in the specified direction
        GetComponent<Rigidbody2D>().velocity = new Vector2(playerDirection.x * 10, playerDirection.y * 10);

        yield return new WaitForSeconds(1f);
        
        transform.rotation = startRotation;

        OnAttackComplete?.Invoke();
    }
}
