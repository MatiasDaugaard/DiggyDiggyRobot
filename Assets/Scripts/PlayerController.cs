﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float verticalSpeed;
    public float horisontalSpeed;
    public GameObject bullet;
    public GameObject hpbar;
    public GameObject moneybar;
    

    //Player variables
    private Rigidbody rb;
    private float health;
    private float speed;
    private int money;
    private Hashtable bag;

    //Weapon variables
    private GameObject weapon;
    private float weaponDamage;


    //Mining Variables
    private GameObject blockBeingMined;
    private long timer;
    private int mineCount;
    private int mineCountMax;
    private long mineTimer;
    private float disappearingsRate;
    private float miningDistanceY;
    private float miningDistanceZ;
    private int miningSpeed;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = 20.0f;
        hpbar.GetComponent<Text>().text = "HP: " + health.ToString("n2");
        moneybar.GetComponent<Text>().text = money + "$";


        weapon = GameObject.FindGameObjectWithTag("Weapon");
        weapon.SetActive(false);

        blockBeingMined = null;
        
        mineCount = 0;
        miningDistanceY = 0.0f;
        miningDistanceZ = 0.0f;
        miningSpeed = 40;

        UpdateMining(miningSpeed);

    }

    private void UpdateMining(int max)
    {
        mineCountMax = max;
        mineTimer = 100 / mineCountMax;
        disappearingsRate = 1.0f / mineCountMax;
    }

    private void Mine()
    {
        mineCount++;
        rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y + miningDistanceY, rb.transform.position.z + miningDistanceZ);
        timer = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
        Color col = blockBeingMined.GetComponent<Renderer>().material.color;
        blockBeingMined.GetComponent<Renderer>().material.color = new Color(col.r, col.g, col.b, col.a - disappearingsRate);
    }

    private void FixedUpdate()
    {
        if (blockBeingMined != null)
        {
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            rb.isKinematic = true;
            if (System.DateTimeOffset.Now.ToUnixTimeMilliseconds() - timer > mineTimer)
            {
                Mine();
            }
            if (mineCount == mineCountMax)
            {
                blockBeingMined.SetActive(false);
                blockBeingMined = null;
                mineCount = 0;
                rb.isKinematic = false;
            }
        }
        else
        {
            if (Input.anyKey)
            {
                if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
                {
                    float moveHorizontal = Input.GetAxis("Horizontal");
                    float moveVertical = Input.GetAxis("Vertical");
                    Vector3 movement = new Vector3(0.0f, moveVertical * verticalSpeed, moveHorizontal * horisontalSpeed);

                    rb.AddForce(movement);
                    
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (weapon.activeSelf)
                    {
                        weapon.SetActive(false);
                    }
                    else
                    {
                        weapon.SetActive(true);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D) && weapon.activeSelf/* && cooledDown()maybe?*/)
                {
                    GameObject bulletInstance = Instantiate(bullet);
                    bulletInstance.transform.position = weapon.transform.position + new Vector3(0.0f, 0.0f, 0.225f);
                    bulletInstance.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, 2000.0f));
                }
                else if (Input.GetKeyDown(KeyCode.A) && weapon.activeSelf)
                {
                    GameObject bulletInstance = Instantiate(bullet);
                    bulletInstance.transform.position = weapon.transform.position + new Vector3(0.0f, 0.0f, -0.225f);
                    bulletInstance.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, -2000.0f));
                }
            }
        }
    }

    private void Update()
    {
        weapon.transform.position = gameObject.transform.position + new Vector3(0.0f, 0.35f, 0.0f);
        speed = rb.velocity.y;
    }

    private void StartMining(GameObject block, char dir)
    {
        if (!weapon.activeSelf)
        {
            timer = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
            blockBeingMined = block;
            if (dir == 'D')
            {
                miningDistanceY = (block.transform.position.y - rb.transform.position.y) / mineCountMax;
            }
            else
            {
                miningDistanceY = 0.0f;
            }

            miningDistanceZ = (block.transform.position.z - rb.transform.position.z) / mineCountMax;
            UpdateMining((int)(miningSpeed + ((-1.0f * block.transform.position.y) / 5)));
        }
    }

    private bool BlockIsBelow(Vector3 blockPos, Vector3 playerPos)
    {
        float blockSize = 0.5f;
        return blockPos.y < playerPos.y - blockSize / 2 && (blockPos.z < playerPos.z + blockSize / 2 && blockPos.z > playerPos.z - blockSize / 2);
    }

    private bool BlockIsSameLevel(Vector3 blockPos, Vector3 playerPos)
    {
        float blockSize = 0.5f;
        return (blockPos.y < playerPos.y + blockSize / 2 && blockPos.y > playerPos.y - blockSize / 2);
    }

    private bool BlockIsRight(Vector3 blockPos, Vector3 playerPos)
    {
        float blockSize = 0.5f;
        return blockPos.z > playerPos.z + blockSize / 2;
    }

    private bool BlockIsLeft(Vector3 blockPos, Vector3 playerPos)
    {
        float blockSize = 0.5f;
        return blockPos.z < playerPos.z + blockSize / 2;
    }

    private bool PlayerIsFlying(Vector3 blockPos, Vector3 playerPos)
    {
        return rb.velocity.y >= 0.0005f || rb.velocity.y <= -0.0005f || playerPos.y >= blockPos.y ;
    }

    private void OnCollision(Collision collision)
    {
        Vector3 playerPos = gameObject.transform.position;
        Vector3 blockPos = collision.gameObject.transform.position;

        if (collision.gameObject.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (BlockIsBelow(blockPos, playerPos))
                {
                    StartMining(collision.gameObject, 'D');
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (BlockIsRight(blockPos, playerPos) && BlockIsSameLevel(blockPos, playerPos) && !PlayerIsFlying(blockPos, playerPos))
                {
                    StartMining(collision.gameObject, 'R');
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (BlockIsLeft(blockPos, playerPos) && BlockIsSameLevel(blockPos, playerPos) && !PlayerIsFlying(blockPos, playerPos))
                {
                    StartMining(collision.gameObject, 'L');
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (speed < -5.0f)
        {
            health += speed / 2.0f;
            hpbar.GetComponent<Text>().text = "HP: " + health.ToString("n2") + "  ";
        }
        OnCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        OnCollision(collision);  
    }
}