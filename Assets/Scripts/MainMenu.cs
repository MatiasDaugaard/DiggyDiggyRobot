using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        GlobalControl.Instance.NewGame = true;
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        if (File.Exists(GameData.Path))
        {
            GlobalControl.Instance.NewGame = false;
            SceneManager.LoadScene(1);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
