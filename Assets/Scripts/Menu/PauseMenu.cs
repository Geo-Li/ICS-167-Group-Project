using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Ali Hassan

public class PauseMenu : MonoBehaviour
{
    //Gets the object to turn off pause menu when we want it to be hidden
    [SerializeField] private GameObject pausemenu;
    public static bool IsPaused;

    // Start with the pause menu hidden
    void Start()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
    }

    //Check to see if the game is paused when Esc is pressed
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    //Pause the game and show the menu
    public void PauseGame()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    //Resume the game and hide the menu
    public void ResumeGame()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    //Go Back to Scene 0 (Main Menu)
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    //Closes application
    public void QuitGame()
    {
        Application.Quit();
    }
}
