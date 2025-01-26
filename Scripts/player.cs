using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed;
    private float horizontalScreenLimit;
    private float verticalScreenLimit;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public AudioClip jumpClip;

    public bool isGrounded;
    Rigidbody rb;

    void Start()
    {
        speed = 6f;
        horizontalScreenLimit = 11.5f;
        verticalScreenLimit = 7.5f;
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 0.2f, 0.0f);
    }

    
    void Update()
    {
        Movement();
        Jumping();
    }
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);
        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }

    void Jumping()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
           rb.AddForce(jump * jumpForce, ForceMode.Impulse);
           isGrounded = false;
            PlayJumpClip();
        }

    }

    public void PlayJumpClip()
    {

        AudioSource.PlayClipAtPoint(jumpClip, Camera.main.transform.position);

    }

    private void OnCollisionStay()
    {
        isGrounded = true;
    }
}
