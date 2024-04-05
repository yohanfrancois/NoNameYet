using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    private int score = 0;
    private int level = 1;
    private int scoreBeforeLevelIncreasing = 5;
    private PlayerController playerController;

    public int Level => level;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText();
        UpdateLevelText();
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerController.IsDead)
        {
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + score.ToString();
        }
    }

    void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = "Level : " + level.ToString();
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    public void IncreaseLevel()
    {
        if (score % scoreBeforeLevelIncreasing == 0 && score != 0)
        {
            level++;
            UpdateLevelText();
        }
    }

    public void ResetLevel()
    {
        level = 0;
        UpdateLevelText();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
