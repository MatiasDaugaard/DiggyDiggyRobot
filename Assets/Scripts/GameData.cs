using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class GameData
{
    private float[] position;
    private MineralType[,] world;

    public Vector3 Position
    {
        set {
            position = new float[3];
            position[0] = value.x;
            position[1] = value.y;
            position[2] = value.z;
        }
        get {
            return new Vector3(position[0], position[1], position[2]);
        }
    }

    public static string Path
    {
        get { return Application.persistentDataPath + "game.dat"; }
    }

    public static void SaveGame()
    {
        // Collect game data
        GameData data = new GameData();
        Spawn spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawn>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        data.Position = player.transform.position;
        data.world = spawner.World;

        // Serialize data
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = File.Create(Path))
        {
            formatter.Serialize(stream, data);
        }
    }

    public static void LoadGame()
    {
        // Deserialize data
        BinaryFormatter formatter = new BinaryFormatter();
        GameData data = null;
        using (FileStream stream = File.Open(Path, FileMode.Open))
        {
            data = formatter.Deserialize(stream) as GameData;
        }

        // Restore game state
        Spawn spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawn>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = data.Position;
        spawner.LoadWorld(data.world);

    }

}
