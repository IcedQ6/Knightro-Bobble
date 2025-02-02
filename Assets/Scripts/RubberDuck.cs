using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubberDuck : MonoBehaviour
{
    public GameObject item;
    public AudioClip itemClip;
    public float itemLifeTime = 10f;
    public Sprite[] sprites;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    public void SpawnItem()
    {
        Instantiate(item, transform.position, Quaternion.identity);
        Destroy(this.gameObject, itemLifeTime);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            PlayItemClip();
            GameObject.Find("Game Manager").GetComponent<GameManager>().GetScore(100);
            Debug.Log("you collected me");
            Destroy(this.gameObject);
        }
    }

    void PlayItemClip()
    {
        AudioSource.PlayClipAtPoint(itemClip, Camera.main.transform.position, .5f);
    }

}
