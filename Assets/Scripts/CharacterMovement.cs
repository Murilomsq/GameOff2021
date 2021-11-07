using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private CharacterController c;

    private void Start()
    {
        c = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        float i = Input.GetAxis("Vertical");
        float j = Input.GetAxis("Horizontal");
        
        if (i != 0 || j != 0)
        {
            print("all");
            c.Move(new Vector3(-i-j,0, -i+j) * 5 * Time.deltaTime);
        }
    }
}
