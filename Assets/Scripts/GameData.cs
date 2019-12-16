using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class GameData
{
    private float[] position;
    private float[] speed;
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
        PlayerController controller = player.GetComponent<PlayerController>();

        data.Position = player.transform.position;
        data.speed = new float[2];
        data.speed[0] = controller.horisontalSpeed;
        data.speed[1] = controller.verticalSpeed;
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
        PlayerController controller = player.GetComponent<PlayerController>();

        player.transform.position = data.Position;
        controller.horisontalSpeed = data.speed[0];
        controller.verticalSpeed = data.speed[1];
        spawner.LoadWorld(data.world);

    }

}
