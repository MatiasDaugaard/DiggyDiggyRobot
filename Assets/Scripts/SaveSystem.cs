using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public Spawn spawn;

    public void Start()
    {
        if (GlobalControl.Instance != null)
        {
            if (GlobalControl.Instance.NewGame)
            {
                spawn.GenerateWorld();
                SaveGame();
            }
            else
            {
                LoadGame();
            }
        }
    }

    public void SaveGame()
    {
        GameData.SaveGame();
    }

    public void LoadGame()
    {
        GameData.LoadGame();
    }
}
