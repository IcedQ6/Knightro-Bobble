using System.Collections;
using UnityEditor.Build;
using UnityEngine;

public class FallThrough : MonoBehaviour
{
    private Collider2D playerCollider;
    private bool isPlayerOnPlat;

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        //checks if player player is both on the stage AND inputting negative vertical values AKA Down
        if (isPlayerOnPlat && Input.GetAxisRaw("Vertical") < 0)
        {
            playerCollider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }
    //time it will take for the player to fall through the stage
    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(1f);
        playerCollider.enabled = true;

    }

    //fetches player script to validate if the colliding force was the player
    private void AllowPlayerOnPlat(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<player>();
        if (player != null)
        {
            isPlayerOnPlat = value;
        }
    }
    //Determines if the player is colliding with stage
    private void OnCollisionEnter2D(Collision2D other)
    {
            AllowPlayerOnPlat(other, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        AllowPlayerOnPlat(other, true);
    }

}
