using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastRoomba : MonoBehaviour
{
    public float maxDistance = 2f;
    public float roombaSpeed = 4f;

    // Update is called once per frame
    void Update()
    {
        Ray roombaRay = new Ray(transform.position, transform.forward);

        Debug.DrawRay(roombaRay.origin, roombaRay.direction, Color.magenta);

        if (Physics.Raycast(roombaRay, maxDistance))
        {
            int randomNumber = Random.Range(0, 100);
            if (randomNumber <= 49)
            {
                transform.Rotate(0, 90, 0);
            }
            else
            {
                transform.Rotate(0, -90, 0);
            }
        }
        else
        {
            transform.Translate(0, 0, Time.deltaTime * roombaSpeed);
        }
    }
}
