using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Ali Hassan

public class MainMenu : MonoBehaviour
{

    public static bool isMultiplayer;

    //Loads Next Scene in Game (main level)
    public void PlayGame()
    {
        isMultiplayer = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MultiplayerScene()
    {
        isMultiplayer = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    //Closes Game
    public void QuitGame()
    {
        Application.Quit();
    }
}
