using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public Vector3 spawnLoc;

    //public bool activeCheckpoint;

    public GameObject kid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Kid"))
        {
            Debug.Log("Collision!");
            //kid.GetComponent<KidController>().mySpawn.toRespawn = this;
            kid.GetComponent<KidController>().mySpawn.RefreshRespawn(this);
        }
    }
}
