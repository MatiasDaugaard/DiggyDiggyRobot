using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class GameData
{
    // Player controller
    private float[] position;
    private float[] velocity;
    private float[] rotation;
    private float[] angularVelocity;

    // Player data
    private float health;
    private float maxHealth;
    private float speedMultiplier;
    private float fuel;
    private float maxFuel;
    private float weaponDamage;
    private int miningSpeed;
    private bool[] armorUpgrades;
    private bool[] fuelUpgrades;
    private bool[] jetUpgrades;
    private bool[] weaponUpgrades;
    private bool[] drillUpgrades;

    // Inventory data
    private Dictionary<MineralType, int> inventory;

    // World data
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

    public Vector3 Velocity
    {
        set
        {
            velocity = new float[3];
            velocity[0] = value.x;
            velocity[1] = value.y;
            velocity[2] = value.z;
        }
        get
        {
            return new Vector3(velocity[0], velocity[1], velocity[2]);
        }
    }

    public Quaternion Rotation
    {
        set
        {
            rotation = new float[4];
            rotation[0] = value.x;
            rotation[1] = value.y;
            rotation[2] = value.z;
            rotation[3] = value.w;
        }
        get
        {
            return new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
        }
    }

    public Vector3 AngularVelocity
    {
        set
        {
            angularVelocity = new float[3];
            angularVelocity[0] = value.x;
            angularVelocity[1] = value.y;
            angularVelocity[2] = value.z;
        }
        get
        {
            return new Vector3(angularVelocity[0], angularVelocity[1], angularVelocity[2]);
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
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerController controller = playerObject.GetComponent<PlayerController>();
        Player player = controller.Player;

        // Player controller
        data.Position = controller.Rigidbody.position;
        data.Velocity = controller.Rigidbody.velocity;
        data.Rotation = controller.Rigidbody.rotation;
        data.AngularVelocity = controller.Rigidbody.angularVelocity;

        // Player data
        data.health = player.Health;
        data.maxHealth = player.maxHealth;
        data.speedMultiplier = player.speedMultiplier;
        data.fuel = player.Fuel;
        data.maxFuel = player.maxFuel;
        data.weaponDamage = player.weaponDamage;
        data.miningSpeed = player.miningSpeed;
        data.armorUpgrades = player.ArmorUpgrades;
        data.fuelUpgrades = player.FuelUpgrades;
        data.jetUpgrades = player.JetUpgrades;
        data.weaponUpgrades = player.WeaponUpgrades;
        data.drillUpgrades = player.DrillUpgrades;

        // Inventory data
        data.inventory = controller.inventory.Inventory;

        // World data
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
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerController controller = playerObject.GetComponent<PlayerController>();
        Player player = controller.Player;

        // Player controller
        controller.Rigidbody.position = data.Position;
        controller.Rigidbody.velocity = data.Velocity;
        controller.Rigidbody.rotation = data.Rotation;
        controller.Rigidbody.angularVelocity = data.AngularVelocity;

        // Player data
        player.Health = data.health;
        player.maxHealth = data.maxHealth;
        player.speedMultiplier = data.speedMultiplier;
        player.Fuel = data.fuel;
        player.maxFuel = data.maxFuel;
        player.weaponDamage = data.weaponDamage;
        player.miningSpeed = data.miningSpeed;
        player.ArmorUpgrades = data.armorUpgrades;
        player.FuelUpgrades = data.fuelUpgrades;
        player.JetUpgrades = data.jetUpgrades;
        player.WeaponUpgrades = data.weaponUpgrades;
        player.DrillUpgrades = data.drillUpgrades;

        // Inventory data
        controller.inventory.Inventory = data.inventory;

        // World data
        spawner.LoadWorld(data.world);

    }

}
