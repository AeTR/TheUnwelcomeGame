using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidController : MonoBehaviour
{
    //public GameObject standCap, crouchCap;
    public CapsuleCollider crouchHit, standHit;
    public Camera standCam, crouchCam;
    public float mouseX, mouseY, standSpeed, crouchSpeed, forwardBackward, leftRight;
    public bool canCrouch;
    public Vector3 inputVector, testForward, testRight;
    public Rigidbody myRB;

    public enum Stance
    {
        Standing,
        Crouching
    }

    public Stance myStance;
    // Start is called before the first frame update
    void Awake()
    {
        myStance = Stance.Standing;
    }

    // Update is called once per frame
    void Update()
    {
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
}
