using System.Collections;

using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    public float attackDuration = 1f;
    public float dashSpeed = 15f;

    public float chargeAttackDuration = 1f;
    public float backwardsDistance = 1f;

    public float shakeIntensity = 0.15f;
    public float shakeThreshold = 0.8f;

    /*
     * In C#, delegates and events work together to establish a subscription system. 
     * The delegate serves as a blueprint, defining the method signature that any 
     * subscribing method must adhere to.
     * 
     * The AttackCompleteEventHandler delegate, in this case, specifies methods with
     * no parameters and a void return type. Any method subscribing to this delegate 
     * must respect this signature.
     * 
     * The event, named OnAttackComplete, is an instance of the AttackCompleteEventHandler 
     * delegate, meaning that multiple events can be constructed from the same delegate.
     * It allows other parts of the code to subscribe and be notified when an  attack is complete.
     * 
     * The .Invoke() function is used to notify all subscribers about the completion
     * of the enemy attack. It's a mechanism for communication between objects.
     * 
     * Remember, you can create multiple events based on the same delegate.
    */
    public delegate void AttackCompleteEventHandler();
    public event AttackCompleteEventHandler OnAttackComplete;
    public Vector3 playerDirection { get; set; }
    
    // Start enemy attack coroutine
    public IEnumerator AttackCoroutine()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;

        while (elapsedTime < chargeAttackDuration)
        {
            elapsedTime += Time.deltaTime;
            
            // Calculate interpolation factor using cubic easing function
            float t = EaseOutCubic(elapsedTime / chargeAttackDuration);

            // Smoothly move the GameObject based on the interpolation factor
            transform.position = Vector3.Lerp(initialPosition, initialPosition + -playerDirection * backwardsDistance, t);

            if (t > shakeThreshold)
            {
                // Add random shake effect within specified intensity
                float randomX = Random.Range(-shakeIntensity, shakeIntensity);
                float randomY = Random.Range(-shakeIntensity, shakeIntensity);
                transform.position += new Vector3(randomX, randomY, 0);
            }
            yield return null;
        }
        
        yield return StartCoroutine(PerformDashCoroutine());

        // Call subscribers to signal completion of the attack
        OnAttackComplete?.Invoke();
    }

    // Perform a dash as to attack the player
    private IEnumerator PerformDashCoroutine()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float elapsedTime = 0f;
        Vector2 currentPlayerDirection = playerDirection;
        
        while (elapsedTime < attackDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate easing factor for dash movement
            float dashEaseFactor = EaseOutQuint(elapsedTime / attackDuration);

            // Set the velocity of the Rigidbody2D with easing for dashing towards the player
            rb.velocity = new Vector2(currentPlayerDirection.x * dashSpeed * dashEaseFactor, currentPlayerDirection.y * dashSpeed * dashEaseFactor);

            yield return null;
        }
    }


    // Easing function (Cubic Out)
    // See link for visualisation of the CubicEaseOut curve
    // https://forum.unity.com/attachments/graphanimation_1024-gif.240277/
    private float EaseOutCubic(float t)
    {
        // Apply cubic easing function to create a charging-up effect
        t = t - 1;
        return t * t * t + 1;
    }

    // Easing function (Quint Out)
    private float EaseOutQuint(float t)
    {
        t--;
        return 1 + t * t * t * t * t;
    }
}
