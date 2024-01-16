using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Script_Player_Controls : MonoBehaviour
{
    public float moveSpeed, sprintSpeedMultiplier, sprintCost;
    public float dashingPower, dashingTime, dashingCost;

    public Image staminaBar;
    public float stamina, maxStamina;

    private Vector2 movementVector;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool isSprinting;

    private bool canDash = true;
    private bool isDashing = false;
    private bool isInvincible = false;

    void Start()
    {
        Cursor.visible = false;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }  
        else
        {
            isSprinting = false;
        }
    }
    
    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        float currentMoveSpeed = isSprinting ? moveSpeed * sprintSpeedMultiplier : moveSpeed;

        rb.velocity = new Vector2(movementVector.x * currentMoveSpeed, movementVector.y * currentMoveSpeed);

        if (isSprinting)
        {
            stamina -= sprintCost * Time.fixedDeltaTime;
            staminaBar.fillAmount = stamina / maxStamina;

            if (stamina <= 0)
            {
                isSprinting = false;
            }
        }
    }

    //
    // Dash
    // 

    private IEnumerator Dash()
    {
        // Decrease stamina
        stamina -= dashingCost;
        if (stamina < 0) stamina = 0;
        staminaBar.fillAmount = stamina / maxStamina;

        // Temporarily disable dashing
        canDash = false;
        
        isDashing = true;
        isInvincible = true;

        // Change color during invincibility state
        sr.color = Color.black;

        // Apply velocity for the dash in the specified direction
        rb.velocity = new Vector2(movementVector.x * dashingPower, movementVector.y * dashingPower);

        yield return new WaitForSeconds(dashingTime);

        // Change color to original after dash ended
        sr.color = Color.white;

        isDashing = false;
        isInvincible = false;

        // Enable dashing
        canDash = true;
    }
}