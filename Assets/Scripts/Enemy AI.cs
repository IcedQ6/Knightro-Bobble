using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Movement variables
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float patrolRange = 5f;

// Jumping variables
    public float jumpForce = 5f;
    public float jumpInterval = 3f; // Time between jumps
    private float jumpTimer = 0f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    // Player detection
    public float detectionRadius = 8f;
    private Transform player;
    private bool isCaptured = false;

    // Shooting
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f; // Shots per second
    private float fireCooldown = 0f;

    // Internal tracking
    private Vector2 startPosition;
    private bool movingRight = true;
     private Rigidbody2D rb;

    // Captured
    public GameObject ducky;
    public float floatSpeed = 2f;

    void Start()
     {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;

        // Find the player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (!player)
        {
           Debug.LogError("No player found in the scene! Make sure the player has the tag 'Player'.");
        }
    }

    [System.Obsolete]
    void Update()
    {
        if (!player) return;

        if (!isCaptured)
        {
            // Check if enemy is on the ground
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

            // Calculate distance to the player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Decide behavior: Patrol or Chase
            if (distanceToPlayer <= detectionRadius)
            {
                ChasePlayer();
            }
            else
            {
                Patrol();
            }

            // Handles jumping
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpInterval && isGrounded)
            {
                Jump();
                jumpTimer = 0f; // Reset jump timer
            }
        } else
        {
            // What happens when enemy is in capture state
            transform.position += Vector3.up * floatSpeed * Time.deltaTime;
            transform.Translate(Vector2.up * floatSpeed * Time.deltaTime);
        }
    }

    void Patrol()
    {
        float patrolRightBoundary = startPosition.x + patrolRange;
        float patrolLeftBoundary = startPosition.x - patrolRange;

        if (movingRight)
        {
            transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime);
            if (transform.position.x >= patrolRightBoundary)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * patrolSpeed * Time.deltaTime);
            if (transform.position.x <= patrolLeftBoundary)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * chaseSpeed * Time.deltaTime);
        
        if (direction.x > 0 && !movingRight)
        {
            movingRight = true;
            Flip();
        }
        else if (direction.x < 0 && movingRight)
        {
            movingRight = false;
            Flip();
        }
    }

    [System.Obsolete]
    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    public void Capture()
    {
        isCaptured = true;
        isGrounded = false;
        //GetComponent<Collider2D>().enabled = false; // Disable movement
        GetComponent<Rigidbody2D>().gravityScale = 0.2f;
        
    }

    public void Defeat()
    {
        GameObject.Find("Game Manager").GetComponent<GameManager>().EnemyDefeated();
        Instantiate(ducky, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        // Draw patrol range
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(startPosition.x - patrolRange, startPosition.y), new Vector2(startPosition.x + patrolRange, startPosition.y));

        // Draw detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnCollisionEnter2D(Collision2D playerHit)
    {
        if(playerHit.gameObject.tag == "Player")
        {
            if (!isCaptured)
            {
                GameObject.Find("Player(Clone)").GetComponent<player>().PlayerLives();
            }
            else
            {
                Defeat();
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            Capture();
            Debug.Log("A projectile has hit the enemy");

            //GameManager.instance.EnemyDefeated();
        }
    }

}