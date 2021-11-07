using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform muzzle;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectile, muzzle.position, transform.rotation);
        }
    }
}
