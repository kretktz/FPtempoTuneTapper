using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadAndante()
    {
        SceneManager.LoadScene("andante");
        EndGamePanel.isVisible = false;
    }

    public void LoadAllegro()
    {
        SceneManager.LoadScene("allegro");
        EndGamePanel.isVisible = false;
    }

    public void LoadAllegrissimo()
    {
        SceneManager.LoadScene("allegrissimo");
        EndGamePanel.isVisible = false;
    }
}
