using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{

    private float health;
    private float damage;
    private Player player;
    private GameObject playerObject;

    void Start()
    {
        health = 20.0f;
        damage = 1.0f;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        
    }

    
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 playerPos = playerObject.transform.position;
        Vector3 mobPos = gameObject.transform.position;

        
        Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);

        if (mobPos.z < playerPos.z)
        {
            movement += new Vector3(0.0f, 0.0f, 5.0f);
        }
        else if (mobPos.z > playerPos.z)
        {
            movement -= new Vector3(0.0f, 0.0f, 5.0f);
        }
        if(mobPos.y < playerPos.y)
        {
            movement += new Vector3(0.0f, 50.0f, 0.0f);
        }
        rb.AddForce(movement*1.0f);
    }

    public void Damage(float x)
    {
        health -= x;
        if (health <= 0.0f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Player();
        if (obj.tag == "Bullet")
        {
            Damage(player.weaponDamage);
            Destroy(obj);
        }else if(obj.tag == "Player")
        {
            print("Hit player");
            player.Damage(damage);
        }
    }
}
