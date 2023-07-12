using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject gameManager;
    public void PlayGame()
    {
        // SceneManager.LoadScene("My Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
