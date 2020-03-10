using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class KidController : MonoBehaviour
{
    //public GameObject standCap, crouchCap;
    public CapsuleCollider crouchHit, standHit;
    public Camera standCam, crouchCam;
    public float mouseX, mouseY, standSpeed, crouchSpeed, forwardBackward, leftRight;
    public bool canCrouch;
    public Vector3 inputVector, testForward, testRight;
    public Rigidbody myRB;
    public Spawner mySpawn;
    public CheckpointScript[] allCheckpoints;
    public float rayDistance;

    public enum Stance
    {
        Standing,
        Crouching
    }

    public Stance myStance;

    void Awake()
    {
        rayDistance = 1f;
        mySpawn = GameObject.Find("Spawner").GetComponent<Spawner>();
        if (mySpawn.first)
        {
            mySpawn.allCheckpoints = new CheckpointScript[allCheckpoints.Length];
            for (int i = 0; i < allCheckpoints.Length; i++)
            {
                mySpawn.allCheckpoints[i] = allCheckpoints[i];
            }
            mySpawn.toRespawn = allCheckpoints[0];
            mySpawn.first = false;
            //transform.position = mySpawn.transform.position;
        }

        Vector3 spawnPosition = mySpawn.transform.position;
        foreach (CheckpointScript temp in allCheckpoints)
        {
            if (temp.checkpointName == mySpawn.spawnName)
            {
                spawnPosition = temp.transform.position;
            }
        }

        transform.position = spawnPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Ray myRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(myRay.origin, myRay.direction * rayDistance, Color.magenta);
        RaycastHit hitObject;
        if (Physics.Raycast(myRay, out hitObject, rayDistance))
        {
            
            if (Input.GetMouseButtonDown(0) && hitObject.collider.gameObject.CompareTag("Door"))
            {
                hitObject.collider.gameObject.GetComponent<DoorScript>().InteractWithDoor();
            }
            
            /*
            Debug.Log("Hitting");
            if (hitObject.collider.gameObject.CompareTag("Door"))
            {
                
                Debug.Log("Hitting Door");
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Opening Door");
                    hitObject.collider.gameObject.GetComponent<DoorScript>().OpenDoor();
                }
            }
            */
        }
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(0, mouseX, 0);
        standCam.transform.Rotate(-mouseY, 0, 0);
        crouchCam.transform.Rotate(-mouseY, 0, 0);
        forwardBackward = Input.GetAxis("Vertical");
        leftRight = Input.GetAxis("Horizontal");
        testForward = transform.forward;
        testRight = transform.right;
        inputVector = (transform.forward * forwardBackward);
        inputVector += (transform.right * leftRight);
        /*
        if (Math.Abs(Input.GetAxis("Vertical")) > 0.001f && Math.Abs(Input.GetAxis("Horizontal")) > 0.001f)
        {
            inputVector *= 0.7f;
        }
        */
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            switch (myStance)
            {
                case Stance.Standing:
                    //crouchCap.SetActive(true);
                    crouchHit.enabled = true;
                    crouchCam.enabled = true;
                    crouchCam.gameObject.GetComponent<AudioListener>().enabled = true;
                    standCam.enabled = false;
                    standCam.gameObject.GetComponent<AudioListener>().enabled = false;
                    //standCap.SetActive(false);
                    standHit.enabled = false;
                    myStance = Stance.Crouching;
                    break;
                case Stance.Crouching:
                    //standCap.SetActive(true);
                    standHit.enabled = true;
                    standCam.enabled = true;
                    standCam.gameObject.GetComponent<AudioListener>().enabled = true;
                    crouchCam.enabled = false;
                    crouchCam.gameObject.GetComponent<AudioListener>().enabled = false;
                    //crouchCap.SetActive(false);
                    crouchHit.enabled = false;
                    myStance = Stance.Standing;
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        switch (myStance)
        {
            case Stance.Standing:
                myRB.velocity = (50 * Time.fixedDeltaTime * standSpeed * inputVector)+ Physics.gravity * 0.69f;
                break;
            case Stance.Crouching:
                myRB.velocity = (50 * Time.fixedDeltaTime * crouchSpeed * inputVector)+ Physics.gravity * 0.69f;
                break;
        }
    }

    public void Die() //possibly not necessary now I suppose
    {
        Debug.Log("Dying now");
        SceneManager.LoadScene("NormalHouse"); //this would be the scary house
    }
}
