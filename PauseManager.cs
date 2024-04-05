using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused = false;

    private PlayerController playerController;

    public bool IsPaused => isPaused;
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !playerController.IsDead)
        {
            if (isPaused)
            {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1.0f;
        isPaused = false;
        Debug.Log("Menu");  
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
