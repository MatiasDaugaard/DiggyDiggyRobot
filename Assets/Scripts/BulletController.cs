using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(gameObject.transform.position.z < -25.0f || gameObject.transform.position.z > 25.0f || (rb.velocity.z < 0.5f && rb.velocity.z > -0.5f))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //Damage Enemy
            //Destroy(gameObject);
        }
    }
}
