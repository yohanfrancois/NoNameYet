using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float xExplosionOffest = 2f;
    public float yExplosionOffest = -2f;
    public int scoreValue = 1;

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Explode();
            if (!playerController.IsDead)
            {
                UpdateScore();
            }
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (playerController.IsDead)
        {
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        Vector3 positionOffset = new Vector3 (xExplosionOffest, yExplosionOffest, 0);
        GameObject explosion = Instantiate(explosionPrefab, transform.position + positionOffset, Quaternion.identity);
        if (playerController.IsDead)
        {
            Destroy(explosion);
        }
        Destroy(explosion, 2.0f);
    }

    private void UpdateScore()
    {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager != null )
        {
            scoreManager.IncreaseScore(scoreValue);
            scoreManager.IncreaseLevel();
        }
        else
        {
            Debug.LogWarning("ScoreManager not found");
        }
    }
}
