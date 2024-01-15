using UnityEngine;

public class Script_Player_Controls : MonoBehaviour
{
    public float moveSpeed;

    private Vector2 movementVector;
    private Rigidbody2D rb;

    void Start()
    {
        Cursor.visible = false;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementVector = new Vector2(
            Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical")
        );

        movementVector.Normalize();
    }
    
    void FixedUpdate()
    {
        rb.velocity = new Vector2(movementVector.x * moveSpeed, movementVector.y * moveSpeed);
    }
}