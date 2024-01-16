using System.Collections;
using UnityEngine;

public class Script_Player_Controls : MonoBehaviour
{
    public float moveSpeed;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    private Vector2 movementVector;
    private Rigidbody2D rb;

    private bool canDash = true;
    private bool isDashing = false;
    
    void Start()
    {
        Cursor.visible = false;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        movementVector = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        movementVector.Normalize();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    
    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        
        rb.velocity = new Vector2(movementVector.x * moveSpeed, movementVector.y * moveSpeed);
    }

    //
    // Dash
    // 

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(movementVector.x * dashingPower, movementVector.y * dashingPower);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}