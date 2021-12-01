using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniBossScript : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject enemyObj;
    [SerializeField] private float maxHealth; 
    private float health;
    [SerializeField] private float aggroRadius = 20.0f;
    [SerializeField] private SpriteRenderer healthImg;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip hit;
    

    private Transform target;   // Player transform
    private PlayerInteractions playerInteractions;   // Player health management
    private NavMeshAgent nma;

    private float startingSize;

    [SerializeField] private float hitCooldown;
    private float hitAvailable;

    public void Damage(float amount)
    {
        health -= amount;
        Transform ht = healthImg.transform;
        ht.localScale = new Vector3((health / maxHealth) * startingSize, ht.localScale.y, 1);
        Vector3 lp = ht.localPosition;
        lp = new Vector3(((amount/maxHealth)*healthImg.size.x* startingSize * 0.5f), 0, 0);
        ht.Translate(lp, Space.Self);
        if (health <= 0)
        {
            Destroy(enemyObj);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }

    private void Start()
    {
        health = maxHealth;
        target = PlayerInteractions.Instance.transform;
        playerInteractions = PlayerInteractions.Instance;
        nma = GetComponent<NavMeshAgent>();
        startingSize = healthImg.transform.localScale.x;
    }

    private void Update()
    {
        float dist = (target.position - transform.position).magnitude;
        hitAvailable += Time.deltaTime;

        if (dist <= aggroRadius && hitAvailable >= hitCooldown)
        {
            animator.SetBool("Running", true);
            nma.SetDestination(target.position);
            if (dist <= nma.stoppingDistance && hitAvailable >= hitCooldown)
            {
                hitAvailable = 0;
                PlayerInteractions.Instance.generalAudioSource.PlayOneShot(hit);
                animator.SetTrigger("Hit");
                playerInteractions.Damage();
                animator.SetBool("Running", false);
            }
        }
    }
}
