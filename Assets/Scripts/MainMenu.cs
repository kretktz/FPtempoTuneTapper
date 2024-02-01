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
    }

    public void LoadAllegro()
    {
        SceneManager.LoadScene("allegro");
    }

    public void LoadAllegrissimo()
    {
        SceneManager.LoadScene("allegrissimo");
    }
}
