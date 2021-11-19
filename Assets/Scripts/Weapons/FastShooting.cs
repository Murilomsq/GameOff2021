using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FastShooting : MonoBehaviour
{
    [SerializeField] private CharacterController c;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float fireRate = 0.2f;
    private float cdTimer = 0.0f;

    private void Update()
    {
        cdTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && cdTimer >= fireRate)
        {
            cdTimer = 0.0f;
            Instantiate(projectile, muzzle.position, transform.rotation);
        }
    }
}
