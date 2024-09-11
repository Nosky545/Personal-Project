using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] enemyPrefab;
    public GameObject[] pickupPrefab;
    
    private float spawnRange = 11f;

    public int enemyCount;
    public int pickupCount;

    public int waveNumber = 1;

    PlayerController playerController;
    EnemyController enemyController;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyController = enemyPrefab[0].GetComponent<EnemyController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        enemyController.enemySpeed = 2.5f;

        SpawnEnemyWave(waveNumber);
        SpawnPickup();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;

        gameManager.enemyLeftText.text = "Enemy Left: " + enemyCount;
        gameManager.roundText.text = "Round: " + waveNumber;

        if (enemyCount == 0)
        {
            waveNumber++;
            enemyController.enemySpeed *= 1.1f;
            SpawnEnemyWave(waveNumber);
        }

        if (pickupCount == 0 && playerController.hasPower == false)
        {
            SpawnPickup();
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[enemyIndex], GenerateSpawnPosition(), enemyPrefab[enemyIndex].transform.rotation);
        }
    }

    void SpawnPickup()
    {
        int powerupIndex = Random.Range(0, pickupPrefab.Length);
        Instantiate(pickupPrefab[powerupIndex], GenerateSpawnPosition2(), pickupPrefab[powerupIndex].transform.rotation);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosx = Random.Range(-spawnRange, spawnRange);
        float spawnPosz = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosx, 0, spawnPosz);
        return randomPos;
    }

    private Vector3 GenerateSpawnPosition2()
    {
        float spawnPosx = Random.Range(-spawnRange, spawnRange);
        float spawnPosz = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosx, 1, spawnPosz);
        return randomPos;
    }
}
