using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerInteractions>(out PlayerInteractions p))
        {
            // Start wave
        }
        
    }

    private void Update()
    {
    }
}
