using System.Collections;
using UnityEditor.Build;
using UnityEngine;

public class FallThrough : MonoBehaviour
{
    private Collider2D _collider;
    private bool _playerOnPlatform;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        //checks if player player is both on the stage AND inputting negative vertical values AKA Down
        if(_playerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            _collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }
    //time it will take for the player to fall through the stage
    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(1f);
        _collider.enabled = true;

    }

    //fetches player script to validate if the colliding force was the player
    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<player>();
        if (player != null)
        {
            _playerOnPlatform = value;
        }
    }
    //Determines if the player is colliding with stage
    private void OnCollisionEnter2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, true);
    }

}
