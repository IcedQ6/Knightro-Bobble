using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject user;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int maxEnemies = 5; // Maximum allowed enemies in the scene
    private float spawnTimer = 0f;
    public int score;
    

    void Start()
    {
        Instantiate(user, new Vector2(0, -8), Quaternion.identity);
        
    }

    
    void Update()
    {
        //Enemy Spawner
         spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval && GetEnemyCount() < maxEnemies)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnPlayer()
    {

    }

    public void GetScore(int Amount)
    {
        score = score + Amount;
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }
    int GetEnemyCount()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length; // Count enemies in the scene
    }
}
