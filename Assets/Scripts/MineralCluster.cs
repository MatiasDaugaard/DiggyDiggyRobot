using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class MineralCluster : ScriptableObject
{
    public GameObject Block {
        get { return block; }
    }

    public MineralType Type {
        get { return Block.GetComponent<Mineral>().Type; }
    }

    public string Name {
        get { return Type.ToString(); }
    }

    public int Amount {
        get { return amount; }
    }

    public float Probability {
        get { return probability; }
    }

    public float Decay {
        get { return decay; }
    }

    public int MinimumDepth {
        get { return minDepth; }
    }

    public int MaximumDepth {
        get { return maxDepth; }
    }

    [SerializeField]
    private GameObject block;
    [SerializeField]
    private int amount;
    [SerializeField]
    private float probability;
    [SerializeField]
    private float decay;
    [SerializeField]
    private int minDepth;
    [SerializeField]
    private int maxDepth;
   
}