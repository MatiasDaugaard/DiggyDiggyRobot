﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{

    private float health;
    private float damage;
    private float speed;
    private Player player;
    private GameObject playerObject;

    void Start()
    {
        if(gameObject.tag == "Boss")
        {
            health = 200.0f;
            damage = 20.0f;
            speed = 4.0f;
        }
        else
        {
            health = 20.0f;
            damage = 4.0f;
            speed = 1.0f;
        }
        
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
        rb.AddForce(movement*speed);
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Player;
        if (obj.tag == "Bullet")
        {
            Damage(player.weaponDamage);
            Destroy(obj);
        }else if(obj.tag == "Player")
        {
            player.Damage(damage);
            if (gameObject.tag == "Boss")
            {

            }
            else
            {
                
                Destroy(gameObject);
            }
            
        }
    }
}
