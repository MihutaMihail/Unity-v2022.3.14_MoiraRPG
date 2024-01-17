using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Script_Player_Controls : MonoBehaviour
{
    // Movement
    public float normalSpeed, sprintSpeed, sprintCost;
    public float dashPower, dashDuration, dashCost;

    // Stamina
    public Image staminaBar;
    public float stamina, maxStamina, chargeRate;

    private Vector2 movementVector;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Coroutine recharge;

    private bool playerMoving = true;
    private bool canDash = true, isDashing = false;
    private bool canSprint = true, isSprinting = false;
    private bool isInvincible = false;

    void Start()
    {
        Cursor.visible = false;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDashing) return;
        ;
        // Player direction
        movementVector = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
        movementVector.Normalize();

        // Check for player movement
        playerMoving = movementVector == new Vector2(0f, 0f) ? false : true;
        
        // Dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash && playerMoving)
        {
            if (EnoughStamina(dashCost)) StartCoroutine(Dash());
        }
        
        // Sprint 
        isSprinting = (Input.GetKey(KeyCode.LeftShift) && canSprint && playerMoving) ? true : false;
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        // Calculate current player speed
        float currentMoveSpeed = isSprinting ? sprintSpeed : normalSpeed;

        // Set player current speed
        rb.velocity = new Vector2(movementVector.x * currentMoveSpeed, movementVector.y * currentMoveSpeed);

        // Player is sprinting
        if (isSprinting)
        {
            // Decrease stamina
            stamina -= sprintCost * Time.fixedDeltaTime;
            staminaBar.fillAmount = stamina / maxStamina;

            // Ensure that there's only one 'recharge' coroutine happening
            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(StaminaRecharge());

            // Reset when not enough stamina
            if (stamina <= 0)
            {
                stamina = 0;
                canSprint = false;
            }
        }
    }

    private IEnumerator Dash()
    {
        // Decrease stamina
        stamina -= dashCost;
        if (stamina < 0) stamina = 0;
        staminaBar.fillAmount = stamina / maxStamina;

        if (recharge != null) StopCoroutine(recharge);
        recharge = StartCoroutine(StaminaRecharge());

        // Temporarily disable dashing
        canDash = false;

        isDashing = true;
        isInvincible = true;

        // Change color during invincibility state (TEMP)
        sr.color = Color.black;

        // Apply velocity for the dash in the specified direction
        rb.velocity = new Vector2(movementVector.x * dashPower, movementVector.y * dashPower);

        yield return new WaitForSeconds(dashDuration);

        // Change color to original after dash ended (TEMP)
        sr.color = Color.white;

        isDashing = false;
        isInvincible = false;
        
        // Enable dashing
        canDash = true;
    }
    
    private IEnumerator StaminaRecharge()
    {
        yield return new WaitForSeconds(1f);
        
        canSprint = true;
        
        while (stamina < maxStamina)
        {
            stamina += chargeRate / 10;
            if (stamina >= maxStamina) stamina = maxStamina;
            staminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    private bool EnoughStamina(float staminaCost)
    {
        return stamina >= staminaCost;
    }
}