using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Script_Player_Controls : MonoBehaviour
{
    // Movement
    public float normalSpeed = 5, sprintSpeed = 10, sprintCost = 15;
    public float dashPower = 10f, dashDuration = 0.3f, dashCost = 20;

    // Stamina
    public Image staminaBar;
    public float stamina = 100f, maxStamina = 100f, chargeRate = 20f;

    private Vector2 movementVector;
    private Rigidbody2D rb;
    private Coroutine recharge;
    
    private bool playerMoving = true;
    private bool canDash = true, isDashing = false;
    private bool canSprint = true, isSprinting = false;

    void Start()
    {
        Cursor.visible = false;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing) return;
        
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
            if (EnoughStamina(dashCost)) StartCoroutine(DashCoroutine());
        }
        
        // Sprint
        isSprinting = (Input.GetKey(KeyCode.LeftShift) && canSprint && playerMoving) ? true : false;
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        float currentMoveSpeed = isSprinting ? sprintSpeed : normalSpeed;

        // Set player current speed
        rb.velocity = new Vector2(movementVector.x * currentMoveSpeed, movementVector.y * currentMoveSpeed);

        // Player is sprinting
        if (isSprinting)
        {
            DrainStamina(sprintCost * Time.fixedDeltaTime);

            // Reset when not enough stamina
            if (stamina <= 0)
            {
                stamina = 0;
                canSprint = false;
            }
        }
    }

    // Perform a dash in a specific direction coroutine
    private IEnumerator DashCoroutine()
    {
        DrainStamina(dashCost);

        canDash = false;
        isDashing = true;

        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate easing factor for dash movement
            float dashEaseFactor = EaseOutQuint(elapsedTime / dashDuration);

            // Apply velocity for the dash in the specified direction
            rb.velocity = new Vector2(movementVector.x * dashPower * dashEaseFactor, movementVector.y * dashPower * dashEaseFactor);

            yield return null;
        }
        
        canDash = true;
        isDashing = false;
    }

    // Drain stamina from the stamina bar
    private void DrainStamina(float staminaCost)
    {
        // Drain
        stamina -= staminaCost;
        if (stamina < 0) stamina = 0;

        staminaBar.fillAmount = stamina / maxStamina;

        // Recharge
        // Ensure that there's only one coroutine happening
        if (recharge != null) StopCoroutine(recharge);
        recharge = StartCoroutine(StaminaRechargeCoroutine());
    }

    // Recharging stamina coroutine
    private IEnumerator StaminaRechargeCoroutine()
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
    
    // Check if there is enough stamina for a specific action
    private bool EnoughStamina(float staminaCost)
    {
        return stamina >= staminaCost;
    }

    // Easing function (Quint Out)
    private float EaseOutQuint(float t)
    {
        t--;
        return 1 + t * t * t * t * t;
    }
}