using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public void SaveGame()
    {
        GameData.SaveGame();
    }

    public void LoadGame()
    {
        GameData.LoadGame();
    }
}
