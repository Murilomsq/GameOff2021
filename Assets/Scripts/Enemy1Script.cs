using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Script : MonoBehaviour, IDamageable
{
    private float health;
    [SerializeField] private float aggroRadius = 20.0f;
    private Transform target;
    private NavMeshAgent nma;
    

    
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
        Gizmos.DrawSphere(transform.position, aggroRadius);
    }

    private void Start()
    {
        target = PlayerInteractions.Instance.transform;
        nma = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float dist = (target.position - transform.position).magnitude;

        if (dist <= aggroRadius)
        {
            nma.SetDestination(target.position);
        }
    }
}
