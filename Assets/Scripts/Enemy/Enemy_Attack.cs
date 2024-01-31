using System.Collections;

using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    public float chargeAttackDuration = 1f;
    public float attackDuration = 0.5f;    

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
    public Vector2 playerDirection { get; set; }
    
    public void AttackPlayer()
    {
        StartCoroutine(AttackCoroutine());
    }
    
    private IEnumerator AttackCoroutine()
    {
        float elapsedTime = 0f;
        float rotationDuration = chargeAttackDuration;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -90f);

        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // Apply velocity for the attack in the specified direction
        GetComponent<Rigidbody2D>().velocity = new Vector2(playerDirection.x * 15, playerDirection.y * 15);
        
        yield return new WaitForSeconds(attackDuration);
        
        OnAttackComplete?.Invoke();
    }
}
