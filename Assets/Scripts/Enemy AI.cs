using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Movement variables
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float patrolRange = 5f;

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

    void Start()
    {
        startPosition = transform.position;

        // Find the player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (!player)
        {
           Debug.LogError("No player found in the scene! Make sure the player has the tag 'Player'.");
        }
    }

    void Update()
    {
        if (!player) return;
        if (isCaptured) return;

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

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
public void Capture()
    {
        isCaptured = true;
        GetComponent<Collider2D>().enabled = false; // Disable movement
        GetComponent<Rigidbody2D>().gravityScale = 0;
        transform.position += Vector3.up * 0.1f; // Slight floating effect
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
            GameObject.Find("Player(Clone)").GetComponent<player>().PlayerLives();

        }

    }

    private void OnCollisiobEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Weapon")
        {
            Destroy(this.gameObject);
            GameObject.Find("Item").GetComponent<RubberDuck>().SpawnItem();
        }
    }

}