using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed;
    private float horizontalScreenLimit;
    private float verticalScreenLimit;
    public Vector2 jump;
    public float jumpForce = 5.0f;
    public AudioClip jumpClip;
    public bool isGrounded = false;
    Rigidbody2D rb;
    void Start()
    {
        speed = 6f;
        horizontalScreenLimit = 11.5f;
        verticalScreenLimit = 7.5f;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Movement();
        Jumping();
    }
    //Allows the player to move just left and right, while also allowing the player to return from the top if they fall or from side to side if desired.
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, 0, 0) * Time.deltaTime * speed);
        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }
    //Allows the player to jump while also playing its SFX
    void Jumping()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame && isGrounded == false)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            PlayJumpClip();
        }  
    }

    void Shooting()
    {
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {


        }

    }

    //SFX
    public void PlayJumpClip()
    {
        AudioSource.PlayClipAtPoint(jumpClip, Camera.main.transform.position);
    }
    //These two functions validate if the player is touching the stage.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Stage")
        {
            isGrounded = false;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Stage")
        {
            isGrounded = true;
        }
    }
}
