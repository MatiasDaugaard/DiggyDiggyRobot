using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public SaveSystem saveSystem;

    public void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void Continue()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void SaveGame()
    {
        saveSystem.SaveGame();
        Continue();
    }

    public void LoadGame()
    {
        saveSystem.LoadGame();
        Continue();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
