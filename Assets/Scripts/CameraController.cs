using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public float distance;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = new Vector3(target.transform.position.x+distance, target.transform.position.y, target.transform.position.z);
        
    }
}

