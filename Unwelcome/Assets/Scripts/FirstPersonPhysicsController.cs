using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonPhysicsController : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        crouchHeight = standingHeight - 0.25f;
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
                    myCrouchState = CrouchState.EnteringCrouch;
                    break;
                case CrouchState.Crouching:
                    myCrouchState = CrouchState.LeavingCrouch;
                    break;
                case CrouchState.EnteringCrouch:
                    myCrouchState = CrouchState.LeavingCrouch;
                    break;
                case CrouchState.LeavingCrouch:
                    myCrouchState = CrouchState.EnteringCrouch;
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        if (myCrouchState == CrouchState.EnteringCrouch)
        {
            if (Camera.main.transform.position.y > crouchHeight)
            {
                Camera.main.transform.Translate(new Vector3(0f, -0.05f, 0f));
            }
            else
            {
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
