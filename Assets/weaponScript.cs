using System.Collections;
using UnityEngine;



public class weaponScript : MonoBehaviour
{
    // The weapon has two states: it has hit something or it has not.
    private bool hasHit = false;

    // Since the object can only lose ballons, we only need to switch it to that sprite once it has lost them.
    public Sprite noBallons;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public float weaponSpeed = 50f;
    private float timeToLive = 8f;

    private void Start()
    {
        StartCoroutine(delayedDespawn());
        if(GameObject.Find("Player(Clone)").GetComponent<player>().isFacingRight == false)
        {
            weaponSpeed = -weaponSpeed;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveWeapon();
    }

    IEnumerator delayedDespawn()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }

    void moveWeapon()
    {
        Vector3 newPos = Vector3.zero;
        newPos.x = weaponSpeed * Time.deltaTime;
        transform.position = transform.position + newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && hasHit == false)
        {
            hasHit = true;
            spriteRenderer.sprite = noBallons;
            Debug.Log("Projectile has collided with something");
        }
    }
}
