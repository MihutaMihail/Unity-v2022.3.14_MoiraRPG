using UnityEngine;

[RequireComponent(typeof(Enemy_Follow))]
[RequireComponent(typeof(Enemy_Attack))]
public class EnemyController : MonoBehaviour
{
    public float speed = 3;

    private Enemy_Follow enemyFollow;
    // private Enemy_Attack enemyAttack;

    private const float ReturnThreshold = 0.1f;
    
    private Rigidbody2D rb;
    private Vector3 startPosition;
    private Transform player;

    private Vector2 playerDirection;
    private Vector2 returnDirection;
    private bool isFollowingPlayer = false;
    private bool isReturningToStartingPosition = false;

    void Start()
    {
        InitializeComponents();

        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    
    void Update()
    {
        if (isFollowingPlayer) playerDirection = enemyFollow.CalculateDirection(player.position);
        else if (isReturningToStartingPosition) returnDirection = enemyFollow.CalculateDirection(startPosition);
    }
    
    void FixedUpdate()
    {
        if (isFollowingPlayer) enemyFollow.MoveToPosition(playerDirection, rb, speed);
        else if (isReturningToStartingPosition) enemyFollow.ReturnToStartingPosition(returnDirection, rb, speed, ReturnThreshold, ref isReturningToStartingPosition);
    }

    private void InitializeComponents()
    {
        enemyFollow = GetComponent<Enemy_Follow>();
        // enemyAttack = GetComponent<Enemy_Attack>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isFollowingPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFollowingPlayer = false;
            isReturningToStartingPosition = true;
        }
    }
}