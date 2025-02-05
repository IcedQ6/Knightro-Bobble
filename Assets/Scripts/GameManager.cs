using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton to keep GameManager persistent across scenes
    public GameObject user;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int maxEnemies = 5; // Maximum allowed enemies in the scene
    private float spawnTimer = 0f;
    public int score;
     public int enemiesToDefeat = 3; // Required enemies to defeat to progress
    private int enemiesDefeated = 0;
    private bool levelComplete = false;
    private string currentLevel;
    [SerializeField] private TextMeshProUGUI ScoreDisplay;

    void Awake()
    {
        // Singleton Pattern: Ensures GameManager persists between levels
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps GameManager when switching scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Instantiate(user, new Vector2(0, -8), Quaternion.identity);
        currentLevel = SceneManager.GetActiveScene().name;
        Debug.Log("Current Level: " + currentLevel);
    }

    
    void Update()
    {
         if (levelComplete) return; //stops spawner
        //Enemy Spawner
         spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval && GetEnemyCount() < maxEnemies)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
        ScoreDisplay.text = "Score: " + (score.ToString());
    }

    void SpawnPlayer()
    {

    }

    public void GetScore(int Amount)
    {
        score = score + Amount;
        
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        //AddScore(100); // Example: 100 points per enemy

        if (enemiesDefeated >= enemiesToDefeat)
        {
            levelComplete = true;
            Debug.Log("All enemies defeated! Transitioning in 10 seconds...");
            Invoke("HandleLevelCompletion", 10f); // Wait 10 seconds before deciding next step
        }
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
        void HandleLevelCompletion()
        {
        if (currentLevel == "LevelOne")
            {
            Debug.Log("Loading LevelTwo...");
            SceneManager.LoadScene("LevelTwo");
            }
        else if (currentLevel == "LevelTwo")
            {
            Debug.Log("Game Beaten! Returning to Main Menu...");
            SceneManager.LoadScene("Menu");
            }
        }
    }
