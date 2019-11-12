using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject go;

    public void GenerateWorld()
    {
        // Cleanup
        while (transform.childCount > 0)
        {
            GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
        }

        // Generate blocks
        for (int y = -20; y < 0; y++)
        {
            for (int z = -10; z <= 10; z++)
            {
                GameObject instance = Instantiate(go);
                Transform t = instance.transform;
                t.parent = transform;
                t.position = new Vector3(0.25f, 0.5f * y, z * 0.5f);
            }
        }
    }
}
