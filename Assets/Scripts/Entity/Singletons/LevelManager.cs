using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Entity;
using TMPro;

public class LevelManager : Singleton<LevelManager>
{
    private const float COUNTDOWN_TIME = 40f;

    [Header("Nodes")]
    [SerializeField] private GameObject nodesPrefab;
    [SerializeField] private int nodeCountX;
    [SerializeField] private int nodeCountY;

    [Header("Enemy Spawner")]
    [SerializeField] private GameObject enemySpawnerPrefab;

    [Header("Enemy Despawner")]
    [SerializeField] private GameObject enemyDespawnerPrefab;
    [SerializeField] private int enemyDespawnerSpawnX;
    [SerializeField] private int enemyDespawnerSpawnY;

    [Header("Components")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button returnToMainMenuButton1;
    [SerializeField] private Button returnToMainMenuButton2;
    [SerializeField] private TextMeshProUGUI countdownText;
    private float countdown;

    // Start is called before the first frame update
    void Start()
    {
        returnToMainMenuButton1.onClick.AddListener(() =>
        {
            LoadingManager.Instance.LoadScene((int)SceneIndices.Menu);
        });
        returnToMainMenuButton2.onClick.AddListener(() =>
        {
            LoadingManager.Instance.LoadScene((int)SceneIndices.Menu);
        });

        Nodes nodes = Instantiate(
            original: nodesPrefab,
            position: nodesPrefab.transform.position,
            rotation: nodesPrefab.transform.rotation).GetComponent<Nodes>();
        nodes.Initialize(nodeCountX, nodeCountY);

        countdown = COUNTDOWN_TIME;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f)
        {
            EnemySpawner enemySpawner = Instantiate(
                original: enemySpawnerPrefab,
                position: enemySpawnerPrefab.transform.position,
                rotation: enemySpawnerPrefab.transform.rotation).GetComponent<EnemySpawner>();

            // Randomize the enemy spawner location
            enemySpawner.Spawn(UnityEngine.Random.Range(0, Mathf.FloorToInt(nodeCountX / 4)), UnityEngine.Random.Range(0, nodeCountY));

            EnemyDespawner enemyDespawner = Instantiate(
                original: enemyDespawnerPrefab,
                position: enemyDespawnerPrefab.transform.position,
                rotation: enemyDespawnerPrefab.transform.rotation).GetComponent<EnemyDespawner>();

            // Randomize the enemy despawner location
            enemyDespawner.Spawn(UnityEngine.Random.Range(Mathf.FloorToInt(3 * nodeCountX / 4), nodeCountX), UnityEngine.Random.Range(0, nodeCountY));

            enemySpawner.SetPath(enemyDespawner);
            
            countdown = COUNTDOWN_TIME;
            StartCoroutine(enemySpawner.SpawnEnemyWave(enemyDespawner));
        }

        countdown = Mathf.Max(0f, countdown - Time.deltaTime);
        countdownText.text = countdown.ToString("F2"); // 2 decimal points
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
