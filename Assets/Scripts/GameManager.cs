using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject user;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;

    private float spawnTimer = 0f;
    

    void Start()
    {
        Instantiate(user, transform.position, Quaternion.identity);
        
    }

    
    void Update()
    {
        //Enemy Spawner
      //   spawnTimer += Time.deltaTime;
      //  if (spawnTimer >= spawnInterval)
     //   {
     //       SpawnEnemy();
       //     spawnTimer = 0f;
   // }
 //spawnTimer += Time.deltaTime;
   //     if (spawnTimer >= spawnInterval)
     //   {
       //     SpawnEnemy();
         //   spawnTimer = 0f;
        //}
    }

   // void SpawnEnemy()
    //{
      //  int randomIndex = Random.Range(0, spawnPoints.Length);
      //  Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    //}
}
