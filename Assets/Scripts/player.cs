using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    // Weapon object
    public GameObject weapon;
    // Screen
    private float horizontalScreenLimit;
    private float verticalScreenLimit;
    // Player effects
    private float horizontalInput;
    private float verticalInput;
    public float speed = 5;
    public Vector2 jump;
    public float jumpForce = 9.5f;
    private int input;
    private int lives;
    // Bools
    public bool isGrounded = false;
    public bool isFacingRight = false;
    // Sprites
    public SpriteRenderer spriteRenderer;
    // Colliders
    Rigidbody2D rb;
    // SFX
    public AudioClip jumpClip;
    public AudioClip deathClip;
    public AudioClip damageClip;
    public AudioClip weaponClip;
    // Animation
    private Animator animator;

    void Start()
    {

        horizontalScreenLimit = 11.5f;
        verticalScreenLimit = 12f;
        rb = GetComponent<Rigidbody2D>();
        lives = 3;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Movement();
        Jumping();
        Shooting();
    }



    //Allows the player to move just left and right, while also allowing the player to return from the top if they fall or from side to side if desired.
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();

        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }

        transform.Translate(new Vector3(horizontalInput, 0, 0) * Time.deltaTime * speed);
        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }

        if (horizontalInput != 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }



    }

    void Flip()
    {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }

    //Allows the player to jump while also playing its SFX
    void Jumping()
    {
        if ((Keyboard.current.zKey.wasPressedThisFrame || Keyboard.current.nKey.wasPressedThisFrame) && isGrounded == false)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            PlayJumpClip();
        }
        //Flips the sprite  

    }

    void Shooting()
    {
        if (Keyboard.current.xKey.wasPressedThisFrame || Keyboard.current.mKey.wasPressedThisFrame)
        {
            Instantiate(weapon, transform.position, Quaternion.identity);
            animator.SetTrigger("Throw");
            PlayWeaponClip();
        }

    }

    //Manages player lives
    public void PlayerLives()
    {
        lives--;
        Debug.Log("lost a life");
        PlayDamageClip();
        if (lives == 0)
        {
            Destroy(this.gameObject);
            PlayDeathClip();
        }
    }

    //SFX
    public void PlayJumpClip()
    {
        AudioSource.PlayClipAtPoint(jumpClip, Camera.main.transform.position, .3f);

    }

    public void PlayDeathClip()
    {
        AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position, .5f);
    }

    public void PlayDamageClip()
    {
        AudioSource.PlayClipAtPoint(damageClip, Camera.main.transform.position);
    }

        void PlayWeaponClip()
        {
            AudioSource.PlayClipAtPoint(weaponClip, Camera.main.transform.position, .5f);
        }

        //These two functions validate if the player is touching the stage.
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Stage")
            {
                isGrounded = false;
            }
        }
        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.tag == "Stage")
            {
                isGrounded = true;
            }
        }
    }


