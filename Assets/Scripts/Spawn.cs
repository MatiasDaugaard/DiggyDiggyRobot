using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private List<MineralCluster> clusters;

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
                Vector3 position = new Vector3(0.25f, 0.5f * blockY, blockZ * 0.5f);
                AddBlock(block, position);
            }
        }
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
