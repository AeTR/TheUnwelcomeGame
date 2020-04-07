using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public CheckpointScript[] allCheckpoints;
    public CheckpointScript toRespawn;
    public string spawnName, sceneName;
    public Vector3 spawnLoc;
    public bool first;
    void Awake()
    {
        DontDestroyOnLoad(this);
        first = true;
    }

    void Start()
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void RefreshRespawn(CheckpointScript newRespawn)
    {
        Debug.Log("Refreshing!");
        spawnName = newRespawn.checkpointName;
        toRespawn = newRespawn;
        spawnLoc = toRespawn.transform.position;
    }
}
