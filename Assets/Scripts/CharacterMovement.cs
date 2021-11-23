using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{

    private CharacterController c;
    public float speed;
    private float i;
    private float j;
    private Vector3 v3;
    [SerializeField] private ParticleSystem dashParticles;
    
    [Header("Dash cooldown")] 
    [SerializeField] private Image dashCdImage;
    [SerializeField] private float dashCD = 1.0f;
    private float dashAvailable = 0.0f;
    

    private IEnumerator Dash()
    {
        dashParticles.Play();
        Vector3 V3 = transform.position;
        for (int i = 0; i < 16; i++)
        {
            c.Move(v3.normalized * 0.3f);
            yield return new WaitForSeconds(0.1f/16);
        }
        dashParticles.Stop();
        
    }
    public IEnumerator FillCooldown()
    {
        for (int i = 0; i < 16; i++)
        {
            dashCdImage.fillAmount = -(i / 16.0f) + 1.0f;
            yield return new WaitForSeconds(dashCD/16);
        }
        dashCdImage.fillAmount = 0.0f;
    }

    private void Start()
    {
        c = GetComponent<CharacterController>();
    }

    private void Update()
    {
        dashAvailable += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashAvailable >= dashCD)
        {
            dashAvailable = 0.0f;
            StartCoroutine(Dash());
            StartCoroutine(FillCooldown());
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
