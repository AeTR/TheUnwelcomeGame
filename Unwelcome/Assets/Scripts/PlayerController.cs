using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum CrouchState
    {
        Standing,
        EnteringCrouch,
        Crouching,
        LeavingCrouch
    }
    public CrouchState myCrouchState;
    
    public Rigidbody myRB;

    public float forwardBackward, leftRight;

    public float mouseX, mouseY;

    public Vector3 inputVector;

    public float moveSpeed = 7;

    public float crouchHeight, standingHeight;

    public Vector3 standing, crouching;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        crouchHeight = standingHeight - 0.25f;
        myCrouchState = CrouchState.Standing;
    }

    // Update is called once per frame
    void Update()
    {
        // get mouselook
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        
        transform.Rotate(new Vector3(0, mouseX, 0));
        Camera.main.transform.Rotate(new Vector3(-mouseY, 0, 0));
        
        //up and down on the controller
        forwardBackward = Input.GetAxis("Vertical");
        //left and right on the controller
        leftRight = Input.GetAxis("Horizontal");

        inputVector = transform.forward * forwardBackward;
        inputVector += transform.right * leftRight;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            switch (myCrouchState)
            {
                case CrouchState.Standing:
                    Camera.main.transform.position = Vector3.Lerp(new Vector3((transform.position.x), (transform.position.y + 0.5f), (transform.position.z)),
                        new Vector3((transform.position.x), (transform.position.y + 0.25f), (transform.position.z)), -0.05f);
                    myCrouchState = CrouchState.Crouching;
                    break;
                case CrouchState.Crouching:
                    Camera.main.transform.position = Vector3.Lerp(new Vector3((transform.position.x), (transform.position.y + 0.25f), (transform.position.z)),
                        new Vector3((transform.position.x), (transform.position.y + 0.5f), (transform.position.z)), 0.05f);
                    myCrouchState = CrouchState.Standing;
                    break;
            }
        }
        /*
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            switch (myCrouchState)
            {
                case CrouchState.Standing:
                    myCrouchState = CrouchState.EnteringCrouch;
                    Debug.Log("Moving from Standing to Entering Crouch");
                    break;
                case CrouchState.Crouching:
                    myCrouchState = CrouchState.LeavingCrouch;
                    Debug.Log("Moving from Courching to Leaving Crouch");
                    break;
                case CrouchState.EnteringCrouch:
                    myCrouchState = CrouchState.LeavingCrouch;
                    Debug.Log("Moving from Entering Crouch to Leaving Crouch");
                    break;
                case CrouchState.LeavingCrouch:
                    myCrouchState = CrouchState.EnteringCrouch;
                    Debug.Log("Moving from Leaving Crouch to Entering Crouch");
                    break;
            }
        }
        */
    }

    void FixedUpdate()
    {
        if (myCrouchState == CrouchState.EnteringCrouch)
        {
            if (Camera.main.transform.position.y > crouchHeight)
            {
                /*
                Debug.Log("Moving down");
                Debug.Log(crouchHeight + "    " + Camera.main.transform.position.y);
                Camera.main.transform.Translate(new Vector3(0f, -0.05f, 0f));
                */
            }
            else
            {
                Debug.Log("Now Crouching");
                Debug.Log(crouchHeight + "    " + Camera.main.transform.position.y);
                myCrouchState = CrouchState.Crouching;
            }
        }
        else if (myCrouchState == CrouchState.LeavingCrouch)
        {
            if (Camera.main.transform.position.y < standingHeight)
            {
                Camera.main.transform.Translate(new Vector3(0f, 0.05f, 0f));
            }
            else
            {
                Debug.Log("Now Standing");
                Debug.Log(standingHeight + "    " + Camera.main.transform.position.y);
                myCrouchState = CrouchState.Standing;
            }
        }
        if (Math.Abs(Input.GetAxis("Vertical")) > 0.02f && Math.Abs(Input.GetAxis("Horizontal")) > 0.02f)
        {
            inputVector *= 0.7f;
        }

        if (myCrouchState == CrouchState.Crouching)
        {
            inputVector *= 0.75f;
        }
        myRB.velocity = (50 * Time.fixedDeltaTime * moveSpeed * inputVector)+ Physics.gravity * 0.69f;
    }
}
