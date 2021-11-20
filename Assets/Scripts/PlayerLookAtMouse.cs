using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerLookAtMouse : MonoBehaviour
{
    public Camera auxCam;
    Plane groundPlane = new Plane(Vector3.up, new Vector3(0,1.12f,0));
    void FixedUpdate()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        float rayLength;
 
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);
 
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
