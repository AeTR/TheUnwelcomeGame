using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public HingeJoint myHinge;

    public Animator myAnimator;

    public BoxCollider myCol, openCol, closedCol;

    public enum DoorState
    {
        Open,
        Opening,
        Closed,
        Closing
    }

    public DoorState myState;

    public bool locked, open;

    public Rigidbody myRb;

    public float openForce;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InteractWithDoor();
        }
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
        /*
        if (myState == DoorState.Closed)
        {
            Debug.Log("Door is closed");
            myRb.isKinematic = false;
            Debug.Log("Opening Door");
            myState = DoorState.Opening;
            //myRb.AddTorque(0, openForce, 0);
            myRb.gameObject.transform.Rotate(transform.position, 90);
        }
        else if (myState == DoorState.Open)
        {
            myRb.isKinematic = false;
            myState = DoorState.Closing;
            //myRb.AddTorque(0, -openForce, 0);
            myRb.gameObject.transform.Rotate(transform.position, -90);
        }
        */
        if (open)
        {
            myAnimator.Play("Door Closing");
        }

        else
        {
            myAnimator.Play("Door Opening");
        }
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
