using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        GlobalControl.Instance.NewGame = true;
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        GlobalControl.Instance.NewGame = false;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
