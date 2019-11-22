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
            GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
        }

        // Map blocks
        var map = new Dictionary<MineralType, GameObject>();
        foreach (GameObject blockType in blocks)
        {
            Mineral m = blockType.GetComponent<Mineral>();
            map.Add(m.Type, blockType);
        }

        // Generate matrix
        int width = 20;
        int height = 20;

        // Generate blocks
        GameObject block;
        for (int y = 0; y <= height; y++)
        {
            for (int z = 0; z <= width; z++)
            {
                int blockY = y - height - 1;
                int blockZ = z - width / 2;
                map.TryGetValue(MineralType.Ground, out block);
                GameObject instance = Instantiate(block);
                Transform t = instance.transform;
                t.parent = transform;
                t.position = new Vector3(0.25f, 0.5f * blockY, blockZ * 0.5f);
            }
        }
    }
}
