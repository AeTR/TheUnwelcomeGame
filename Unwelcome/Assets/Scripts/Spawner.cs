using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public CheckpointScript[] allCheckpoints;
    public CheckpointScript toRespawn;
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshRespawn(CheckpointScript newRespawn)
    {
        Debug.Log("Refreshing!");
        toRespawn = newRespawn;
    }
}
