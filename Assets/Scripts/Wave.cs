using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private RoomScript room;
    [SerializeField] private int aliveEnemies;
    
    private void OnTransformChildrenChanged()
    {
        aliveEnemies = transform.childCount;
        if (aliveEnemies == 0)
        {
            room.waves.RemoveAt(0);
            room.NextWave();
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        aliveEnemies = transform.childCount;
    }
}