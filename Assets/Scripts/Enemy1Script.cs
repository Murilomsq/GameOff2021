using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Script : MonoBehaviour, IDamageable
{
    private float health;
    [SerializeField] private float aggroRadius = 20.0f;
    private Transform target;   // Player transform
    private PlayerInteractions playerInteractions;   // Player health management
    private NavMeshAgent nma;

    [SerializeField] private float hitCooldown;
    private float hitAvailable;


    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            // Game over
            print("gameover");
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }

    private void Start()
    {
        target = PlayerInteractions.Instance.transform;
        playerInteractions = PlayerInteractions.Instance;
        nma = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float dist = (target.position - transform.position).magnitude;
        hitAvailable += Time.deltaTime;

        if (dist <= aggroRadius)
        {
            nma.SetDestination(target.position);
            if (dist <= nma.stoppingDistance && hitAvailable >= hitCooldown)
            {
                hitAvailable = 0;
                Debug.Log("Hit");
                playerInteractions.Damage();
            }
        }
    }
}
