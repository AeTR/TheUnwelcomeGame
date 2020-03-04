using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
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
            Debug.Log("Get it");
            GameObject temp = other.gameObject;
            KidController temp2 = temp.GetComponent<KidController>();
            SceneManager.LoadScene("TestScene"); //this would be the scary scene
            //Debug.Log("We would die now possibly");
            //temp2.Die();
        }
    }
}
