using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject groundWall;
    [SerializeField]
    private GameObject airWall;
    [SerializeField]
    private List<MineralCluster> clusters;

    public MineralType[,] World
    {
        get { return null; }
    }

    public void GenerateWorld()
    {
        // Cleanup
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        // Map blocks
        var map = new Dictionary<MineralType, GameObject>();
        foreach (MineralCluster cluster in clusters)
        {
            map[cluster.Type] = cluster.Block;
        }

        // Generate matrix
        int width = 101;
        int height = 100;
        MineralType[,] world = new MineralType[width, height];

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
                AddCluster(world, cluster.Type, new Vector2(x, y), cluster.Probability, cluster.Decay, min, max);
            }
        }

        // Generate blocks
        GameObject block;
        for (int y = 0; y < height; y++)
        {
            for (int z = 0; z < width; z++)
            {
                MineralType blockType = world[z, y];
                int blockY = y - height;
                int blockZ = z - width / 2;
                map.TryGetValue(blockType, out block);
                Vector3 position = new Vector3(0.25f, 0.5f * blockY, 0.5f * blockZ);
                AddBlock(block, position);
            }
        }

        // Generate walls
        GameObject westWall = Instantiate(groundWall);
        Transform westTrans = westWall.transform;
        westTrans.parent = transform;
        westTrans.position = new Vector3(0.25f, -0.5f * (height+1f) / 2f, -0.5f * (width+1f) / 2f);
        westTrans.localScale = new Vector3(westTrans.localScale.x, height / 2f, westTrans.localScale.z);

        GameObject eastWall = Instantiate(groundWall);
        Transform eastTrans = eastWall.transform;
        eastTrans.parent = transform;
        eastTrans.position = new Vector3(0.25f, -0.5f * (height + 1f) / 2f, 0.5f * (width + 1) / 2);
        eastTrans.localScale = new Vector3(eastTrans.localScale.x, height / 2f, eastTrans.localScale.z);

        GameObject southWall = Instantiate(groundWall);
        Transform southTrans = southWall.transform;
        southTrans.parent = transform;
        southTrans.position = new Vector3(0.25f, -0.5f * (height+1), 0f);
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

    private void AddCluster(MineralType[,] world, MineralType mineral, Vector2 location, float probability, float decay, int min, int max)
    {
        int width = world.GetLength(0);
        int height = world.GetLength(1);
        int x = (int) location.x;
        int y = (int) location.y;
        float rand = Random.Range(0f, 1f);
        if (x >= 0 && x < width && y >= min && y < max &&
            world[x, y] == MineralType.Ground && 
            rand < probability)
        {
            world[x, y] = mineral;
            probability *= decay;
            AddCluster(world, mineral, location + new Vector2(0, 1), probability, decay, min, max);
            AddCluster(world, mineral, location - new Vector2(0, 1), probability, decay, min, max);
            AddCluster(world, mineral, location + new Vector2(1, 0), probability, decay, min, max);
            AddCluster(world, mineral, location - new Vector2(1, 0), probability, decay, min, max);
        }
    }

    private void AddBlock(GameObject block, Vector3 position)
    {
        if (block)
        {
            GameObject instance = Instantiate(block);
            Transform t = instance.transform;
            t.parent = transform;
            t.position = position;
        }
    }

    
}
