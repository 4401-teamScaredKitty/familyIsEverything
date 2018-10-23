using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;


    // Update is called once per frame
    void Update()
    {
        /* 
        * If ESC is hit once pause game
        * if hit again resume game
        */

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }


    public void Resume()
    {
        //If game is unpaused resume game speed
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        //freeze game time if paused
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        //Loads Main Menu once clicked
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        //Quits application
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}