using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    private static GlobalControl instance;
    private bool newGame;

    public static GlobalControl Instance
    {
        get { return instance; }
    }

    public bool NewGame
    {
        get { return newGame; }
        set { newGame = value; }
    }

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
