using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private CharacterController c;
    public float speed;

    private void Start()
    {
        c = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        float i = Input.GetAxis("Vertical");
        float j = Input.GetAxis("Horizontal");

        Vector3 v3 = new Vector3(-i - j, 0, -i + j);
        if (i != 0 || j != 0)
        {
            if (v3.magnitude >= 1)
            {
                c.Move(v3.normalized * speed * Time.deltaTime); 
            }
            else
            {
                c.Move(v3 * speed * Time.deltaTime);
            }
            
        }
    }
}
