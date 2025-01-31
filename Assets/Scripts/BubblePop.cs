using UnityEngine;

public class BubblePop : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject); // Pop the bubble

            GameObject trappedEnemy = transform.parent.gameObject;
            if (trappedEnemy.CompareTag("Enemy"))
            {
                Destroy(trappedEnemy); // Defeat the enemy
            }
        }
    }
}
