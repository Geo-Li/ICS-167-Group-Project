using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Loads Next Scene in Game (main level)
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MultiplayerScene()
    {
        SceneManager.LoadScene(2);
    }

    //Closes Game
    public void QuitGame()
    {
        Application.Quit();
    }
}
