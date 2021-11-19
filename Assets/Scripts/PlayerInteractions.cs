using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private static PlayerInteractions instance;
    public static PlayerInteractions Instance { get { return instance; } }

    private int health = 3;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }
    public void Damage()
    {
        health--;
        if (health == 0)
        {
            print("GAME OVER");
            // Game over
        }
    }
}
