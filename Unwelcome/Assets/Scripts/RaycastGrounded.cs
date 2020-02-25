using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGrounded : MonoBehaviour
{
    public float castingDistance = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // declare a ray. at the point of origin and point it down
        Ray myRay = new Ray(transform.position, Vector3.down);
        //set the max distance
        
        //draw a debug line that shows the ray
        Debug.DrawRay(myRay.origin, myRay.direction * castingDistance, Color.magenta);
        
        // shoot the raycast
        if (Physics.Raycast(myRay, castingDistance))
        {
            Debug.Log("Hit Ground");
        }
        else
        {
            transform.Rotate(0, 5f, 0);
        }
    }
}
