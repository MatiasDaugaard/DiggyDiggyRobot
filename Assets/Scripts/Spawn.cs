using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private int width = 101;
    [SerializeField]
    private int height = 100;
    [SerializeField]
    private GameObject groundWall;
    [SerializeField]
    private GameObject airWall;
    [SerializeField]
    private List<MineralCluster> clusters;

    private MineralType[,] world;

    public MineralType[,] World
    {
        get { return world; }
    }

    public Dictionary<MineralType, Mineral> Map
    {
        get
        {
            var map = new Dictionary<MineralType, Mineral>();
            foreach (MineralCluster cluster in clusters)
            {
                map[cluster.Type] = cluster.Block;
            }
            return map;
        }
    }

    public int Width
    {
        get { return width; }
    }

    public int Height
    {
        get { return height; }
    }

    public void Start()
    {
        // Recreate runtime version of world since matrix is not serializable.
        world = new MineralType[width, height];
        foreach (Transform child in transform)
        {
            Mineral mineral = child.GetComponent<Mineral>();
            if (mineral.Location.Length == 2)
            {
                int x = mineral.Location[0];
                int y = mineral.Location[1];
                world[x, y] = mineral.Type;
            }
        }
    }

    public void GenerateWorld()
    {
        // Cleanup
        CleanWorld();

        // Generate matrix
        world = new MineralType[width, height];

        // Fill with ground
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                world[i, j] = MineralType.Ground;
            }
        }

        // Add clusters
        foreach (MineralCluster cluster in clusters)
        {
            for (int i = 0; i < cluster.Amount; i++)
            {
                int x = Random.Range(0, width);
                int min = height - cluster.MaximumDepth;
                int max = height - cluster.MinimumDepth;
                int y = Random.Range(min, max);
                AddCluster(cluster.Type, new Vector2(x, y), cluster.Probability, cluster.Decay, min, max);
            }
        }

        // Generate blocks
        GenerateBlocks();

        // Create world bounds
        GenerateWalls();
    }

    public void LoadWorld(MineralType[,] world)
    {
        // Cleanup
        CleanWorld();

        // Setup data
        this.world = world;

        // Generate blocks
        GenerateBlocks();

        // Create world bounds
        GenerateWalls();
    }

    private void CleanWorld()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    private void GenerateBlocks()
    {
        Mineral block;
        var map = Map;
        for (int y = 0; y < height; y++)
        {
            for (int z = 0; z < width; z++)
            {
                MineralType blockType = world[z, y];
                map.TryGetValue(blockType, out block);
                AddBlock(block, new int[] { z, y });
            }
        }
    }

    private void GenerateWalls()
    {
        GameObject westWall = Instantiate(groundWall);
        Transform westTrans = westWall.transform;
        westTrans.parent = transform;
        westTrans.position = new Vector3(0.25f, -0.5f * (height + 1f) / 2f, -0.5f * (width + 1f) / 2f);
        westTrans.localScale = new Vector3(westTrans.localScale.x, height / 2f, westTrans.localScale.z);

        GameObject eastWall = Instantiate(groundWall);
        Transform eastTrans = eastWall.transform;
        eastTrans.parent = transform;
        eastTrans.position = new Vector3(0.25f, -0.5f * (height + 1f) / 2f, 0.5f * (width + 1) / 2);
        eastTrans.localScale = new Vector3(eastTrans.localScale.x, height / 2f, eastTrans.localScale.z);

        GameObject southWall = Instantiate(groundWall);
        Transform southTrans = southWall.transform;
        southTrans.parent = transform;
        southTrans.position = new Vector3(0.25f, -0.5f * (height + 1), 0f);
        southTrans.localScale = new Vector3(southTrans.localScale.x, southTrans.localScale.y, (width + 2f) / 2f);

        int airHeight = 30;

        GameObject airWestWall = Instantiate(airWall);
        Transform airWestTrans = airWestWall.transform;
        airWestTrans.parent = transform;
        airWestTrans.position = new Vector3(0.25f, 0.5f * airHeight / 2f, -0.5f * (width + 1f) / 2f);
        airWestTrans.localScale = new Vector3(airWestTrans.localScale.x, (airHeight + 1f) / 2f, airWestTrans.localScale.z);

        GameObject airEastWall = Instantiate(airWall);
        Transform airEastTrans = airEastWall.transform;
        airEastTrans.parent = transform;
        airEastTrans.position = new Vector3(0.25f, 0.5f * airHeight / 2f, 0.5f * (width + 1f) / 2f);
        airEastTrans.localScale = new Vector3(airEastTrans.localScale.x, (airHeight + 1f) / 2f, airEastTrans.localScale.z);

        GameObject northWall = Instantiate(airWall);
        Transform northTrans = northWall.transform;
        northTrans.parent = transform;
        northTrans.position = new Vector3(0.25f, 0.5f * (airHeight + 1), 0f);
        northTrans.localScale = new Vector3(northTrans.localScale.x, northTrans.localScale.y, (width + 2f) / 2f);
    }

    private void AddCluster(MineralType mineral, Vector2 location, float probability, float decay, int min, int max)
    {
        int x = (int) location.x;
        int y = (int) location.y;
        float rand = Random.Range(0f, 1f);
        if (x >= 0 && x < width && y >= min && y < max &&
            world[x, y] == MineralType.Ground && 
            rand < probability)
        {
            world[x, y] = mineral;
            probability *= decay;
            AddCluster(mineral, location + new Vector2(0, 1), probability, decay, min, max);
            AddCluster(mineral, location - new Vector2(0, 1), probability, decay, min, max);
            AddCluster(mineral, location + new Vector2(1, 0), probability, decay, min, max);
            AddCluster(mineral, location - new Vector2(1, 0), probability, decay, min, max);
        }
    }

    public void AddBlock(Mineral block, int[] location)
    {
        if (block)
        {
            int y = location[1];
            int z = location[0];
            int blockY = y - height;
            int blockZ = z - width / 2;
            Vector3 position = new Vector3(0.25f, 0.5f * blockY, 0.5f * blockZ);

            Mineral instance = Instantiate(block);
            Transform t = instance.transform;
            t.parent = transform;
            t.position = position;
            instance.Location = location;
        }
    }

    public void RemoveBlock(Mineral block)
    {
        int[] loc = block.Location;
        world[loc[0], loc[1]] = MineralType.Empty;
    }

}
