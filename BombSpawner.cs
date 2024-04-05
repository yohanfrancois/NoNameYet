using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab;
    public float spawnInterval = 1.5f;
    public float timeBeforeChanging = 5f;
    public float decreaseRatio = 1.05f;
    public float minSpawnX = -9f;
    public float maxSpawnX = 9f;
    public float minVelocity = -5f;
    public float maxVelocity = -10f;

    private float elapsedTime = 0f;
    private PlayerController playerController;
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBomb", 0f, spawnInterval);
        playerController = GetComponent<PlayerController>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        if (!playerController.IsDead)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= timeBeforeChanging)
            {
                elapsedTime = 0f;
                DecreaseSpawnInterval();
            }
        }

    }

    private void DecreaseSpawnInterval()
    {
        spawnInterval /= decreaseRatio + Mathf.Pow(scoreManager.Level/10, 2);
        CancelInvoke("SpawnBomb");
        InvokeRepeating("SpawnBomb", spawnInterval, spawnInterval);
        Debug.Log(spawnInterval);
    }

    private void SpawnBomb()
    {
        float randomX = Random.Range(minSpawnX, maxSpawnX);
        float randomVelocity = Random.Range(maxVelocity, minVelocity);

        Vector3 spawnPosition = new Vector3(randomX, 7f, 0f);
        Quaternion spawnRotation = Quaternion.identity;

        GameObject bomb = Instantiate(bombPrefab, spawnPosition, spawnRotation);
        Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();

        bombRb.velocity = new Vector2 (0f, randomVelocity);
    }
}
