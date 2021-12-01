using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStand : MonoBehaviour, IDamageable
{
    
    private Transform target;   // Player transform
    private PlayerInteractions playerInteractions;

    [SerializeField] private GameObject enemyObj;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float shootingCooldown = 3.0f;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private int numOfShots = 3;
    [SerializeField] private Transform muzzle;

    [SerializeField] private float maxHealth; 
    private float health;
    private float startingSize;
    [SerializeField] private SpriteRenderer healthImg;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip hit;

    private void Start()
    {
        health = maxHealth;
        target = PlayerInteractions.Instance.transform;
        playerInteractions = PlayerInteractions.Instance;
        StartCoroutine(BehaviourStandEnemy());
        startingSize = healthImg.transform.localScale.x;
        
    }
    public void Damage(float amount)
    {
        health -= amount;
        Transform ht = healthImg.transform;
        ht.localScale = new Vector3((health / maxHealth) * startingSize, ht.localScale.y, 1);
        Vector3 lp = ht.localPosition;
        Debug.Log(healthImg.size.x);
        lp = new Vector3(((amount/maxHealth)*healthImg.size.x* startingSize * 0.5f), 0, 0);
        ht.Translate(lp, Space.Self);
        if (health <= 0)
        {
            Destroy(enemyObj);
        }
    }

    public IEnumerator BehaviourStandEnemy()
    {
        while (true)
        {
            for (int i = 0; i < numOfShots; i++)
            {
                PlayerInteractions.Instance.generalAudioSource.PlayOneShot(hit);
                Instantiate(projectile, muzzle.position, transform.rotation);
                animator.Play("Fire");
                yield return new WaitForSeconds(fireRate);
            }
            yield return new WaitForSeconds(shootingCooldown);
        }
        

    }
    private void Update()
    {
        transform.LookAt(target);
    }
}
