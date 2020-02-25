using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RaycastMouse : MonoBehaviour
{
    public float maxDistance = 1000f;
    public GameObject paintBrush;
    void Update()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Debug.DrawRay(camRay.origin, camRay.direction * maxDistance, Color.magenta);

        RaycastHit hitObject;

        if (Physics.Raycast(camRay, out hitObject, maxDistance))
        {
            paintBrush.transform.position = hitObject.point;

            if (Input.GetMouseButton(0))
            {
                GameObject paint = Instantiate(paintBrush, hitObject.point, Quaternion.identity);
                paint.transform.SetParent(hitObject.transform);
            }
            hitObject.transform.Rotate(0, 0, 35 * Time.deltaTime);
        }
    }
}
