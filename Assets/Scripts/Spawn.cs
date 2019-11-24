using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> blocks;

    public void GenerateWorld()
    {
        // Cleanup
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        // Map blocks
        var map = new Dictionary<MineralType, GameObject>();
        foreach (GameObject blockType in blocks)
        {
            Mineral m = blockType.GetComponent<Mineral>();
            map.Add(m.Type, blockType);
        }

        // Generate matrix
        int width = 21;
        int height = 20;
        MineralType[,] world = new MineralType[width,height];

        for (int i = 0; i < 5; i++)
        {
            // Insert copper
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
                GameObject instance = Instantiate(block);
                Transform t = instance.transform;
                t.parent = transform;
                t.position = new Vector3(0.25f, 0.5f * blockY, blockZ * 0.5f);
            }
        }
    }
}
