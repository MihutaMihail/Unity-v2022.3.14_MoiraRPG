using System.Collections;

using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    public float chargeAttackDuration = 1f;
    public float attackDuration = 1f;
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
    
    public IEnumerator AttackCoroutine()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;

        while (elapsedTime < chargeAttackDuration)
        {
            elapsedTime += Time.deltaTime;
            
            float t = EaseInOutQuad(elapsedTime / chargeAttackDuration);

            transform.position = Vector3.Lerp(initialPosition, initialPosition + -playerDirection * backwardsDistance, t);

            if (t > shakeThreshold)
            {
                float randomX = Random.Range(-shakeIntensity, shakeIntensity);
                float randomY = Random.Range(-shakeIntensity, shakeIntensity);
                transform.position += new Vector3(randomX, randomY, 0);
            }
            yield return null;
        }
        
        PerformDash();
        
        yield return new WaitForSeconds(attackDuration);
        
        OnAttackComplete?.Invoke();
    }

    private void PerformDash()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(playerDirection.x * 15, playerDirection.y * 15);
    }

    // Custom Easing function (Quadratic InOut)
    // See link for visualisation of the QuadEaseInOut curve
    // https://forum.unity.com/attachments/graphanimation_1024-gif.240277/
    private float EaseInOutQuad(float t)
    {
        return t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;
    } 
}
