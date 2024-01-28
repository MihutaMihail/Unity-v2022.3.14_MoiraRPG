using UnityEngine;

[RequireComponent(typeof(Enemy_Follow))]
[RequireComponent(typeof(Enemy_Attack))]
public class EnemyController : MonoBehaviour
{
    public float speed = 3;
    public float distanceToAttackPlayer = 1f;

    private Enemy_Follow enemyFollow;
    private Enemy_Attack enemyAttack;

    private Rigidbody2D rb;
    private Vector3 startPosition;
    private Transform player;
    
    private Vector2 playerDirection;
    private bool playerInTriggerZone = false;
    private bool isFollowingPlayer = false;
    private bool isReturningToStartingPosition = false;
    private bool canAttack = false;
    private bool isAttackingPlayer = false;

    private bool test = true;

    //
    // START
    //
    
    void Start()
    {
        InitializeComponents();

        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        // Subscribe to OnAttackComplete event
        enemyAttack.OnAttackComplete += HandleAttackComplete;
    }
    
    //
    // UPDATE
    //
    
    void Update()
    {
        if (playerInTriggerZone)
        {
            playerDirection = enemyFollow.CalculateDirection(player.position);
            
            /*
             * Since the enemy attack is happening after a certain duration in the coroutine, 
             * and is only called once, the playerDirecion must be updated so that when 
             * the enemy does attack, it will use an updated value of the playerDirection.
             */
            enemyAttack.playerDirection = playerDirection;
        }

        if (isAttackingPlayer) return;
        
        if (isFollowingPlayer) canAttack = (DistanceToPlayer() < distanceToAttackPlayer) ? true : false;
        
        if (canAttack && test)
        {
            test = false;
            isAttackingPlayer = true; 
            enemyAttack.AttackPlayer();
        }
    }
    
    void FixedUpdate()
    {
        if (isAttackingPlayer) return;

        if (isFollowingPlayer)
            enemyFollow.MoveToPosition(playerDirection, rb, speed);
        else if (isReturningToStartingPosition)
            isReturningToStartingPosition = enemyFollow.ReturnToStartingPosition(startPosition, rb, speed);
    }

    // 
    // GENERAL FUNCTIONS
    //

    private void InitializeComponents()
    {
        // RigidBody
        if (!TryGetComponent(out rb))
        {
            Debug.LogError("Rigidbody2D component not found on the enemy GameObject");
        }

        // Enemy_Follow Script
        if (!TryGetComponent(out enemyFollow))
        {
            Debug.LogError("Enemy_Follow component not found on the enemy GameObject");
        }
        
        // Enemy_Attack Script
        if (!TryGetComponent(out enemyAttack))
        {
            Debug.LogError("Enemy_Attack component not found on the enemy GameObject");
        }
    }

    private float DistanceToPlayer()
    {
        return Vector2.Distance(player.position, transform.position);
    }

    //
    // SUBSCRIBER FUNCTIONS
    //

    private void HandleAttackComplete()
    {
        isAttackingPlayer = false;
    }

    //
    // TRIGGER
    //

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTriggerZone = true;
            isFollowingPlayer = true;
            isReturningToStartingPosition = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTriggerZone = false;
            isFollowingPlayer = false;
            isReturningToStartingPosition = true;
        }
    }
}