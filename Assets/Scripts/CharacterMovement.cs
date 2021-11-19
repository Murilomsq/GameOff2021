using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private CharacterController c;
    public float speed;
    private float i;
    private float j;
    private Vector3 v3;
    [SerializeField] private ParticleSystem dashParticles;
    

    private IEnumerator Dash()
    {
        dashParticles.Play();
        for (int i = 0; i < 16; i++)
        {
            c.Move(v3.normalized * 35.0f * Time.deltaTime); 
            print((v3 * 35.0f * Time.deltaTime).magnitude);
            yield return new WaitForSeconds(0.1f/16);
        }
        dashParticles.Stop();
        
    }

    private void Start()
    {
        c = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            print("a");
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        i = Input.GetAxis("Vertical");
        j = Input.GetAxis("Horizontal");

        v3 = new Vector3(-i - j, 0, -i + j);
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
