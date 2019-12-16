using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    //Mob stuff
    private long timer;
    private float bloodmoonChance;
    public GameObject mob;
    public GameObject boss;
    public GameObject sun;

    private Color normalColor;
    private Color bloodColor;


    
    void Start()
    {
        timer = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
        bloodmoonChance = 0.0f;
        normalColor = sun.transform.GetComponent<Light>().color;
        bloodColor = new Color(166.0f / 255.0f, 19.0f / 255.0f, 5.0f / 255.0f);
    }

    public void Update()
    {
        long now = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (now - timer > 1000*60*5)
        {
            timer = now;
            BloodMoon();
        }
    }

    private void BloodMoon()
    {

        float r = Random.Range(0.0f, 100.0f);
        if (r < bloodmoonChance)
        {
            SpawnMobs();
            bloodmoonChance = 0.0f;
            sun.transform.GetComponent<Light>().color = bloodColor;

        }
        else
        {
            if(bloodmoonChance <= 0.0f)
            {
                sun.transform.GetComponent<Light>().color = normalColor;
            }
            bloodmoonChance += 5.0f;
        }
    }

    private void SpawnMobs()
    {
        int r = (int)Random.Range(5.0f, 20.0f);
        for (int i = 0; i < r; i++)
        {
            GameObject mobInstance = Instantiate(mob);

            Vector3 pos = new Vector3(0.35f, 0.1f, Random.Range(-25.0f, 25.0f));
            mobInstance.transform.position = pos;
        }
    }
}
