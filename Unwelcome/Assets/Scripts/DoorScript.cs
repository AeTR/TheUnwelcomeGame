using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public HingeJoint myHinge;

    public BoxCollider myCol, openCol, closedCol;

    public enum DoorState
    {
        Open,
        Opening,
        Closed,
        Closing
    }

    public DoorState myState;

    public bool locked;

    public Rigidbody myRb;

    public float openForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        /*
        if (myState == DoorState.Opening || myState == DoorState.Closing)
        {
            if (myRb.angularVelocity.magnitude >= 50f)
            {
                if (myState == DoorState.Opening)
                {
                    myState = DoorState.Open;
                    myRb.isKinematic = true;
                }
                else if (myState == DoorState.Closing)
                {
                    myState = DoorState.Closed;
                    myRb.isKinematic = true;
                }
            }
        }
        */
    }

    public void InteractWithDoor()
    {
        if (myState == DoorState.Closed)
        {
            Debug.Log("Door is closed");
            myRb.isKinematic = false;
            Debug.Log("Opening Door");
            myState = DoorState.Opening;
            myRb.AddTorque(0, openForce, 0);
        }
        else if (myState == DoorState.Open)
        {
            myRb.isKinematic = false;
            myState = DoorState.Closing;
            myRb.AddTorque(0, -openForce, 0);
        }
    }

    public void OpenDoor()
    {
        Debug.Log("Continuing to open door");
        myRb.AddTorque(0, openForce, 0);
    }

    /*
    public void OnTriggerEnter(Collider other)
    {
        if(other == closedCol && myState == DoorState.Closing)
        {
            myState = DoorState.Closed;
            myRb.isKinematic = true;
        }
        else if (other == openCol && myState == DoorState.Opening)
        {
            myState = DoorState.Open;
            myRb.isKinematic = true;
        }
    }
    */
}
