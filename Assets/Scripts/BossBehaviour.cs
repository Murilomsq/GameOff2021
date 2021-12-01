using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private bool lookingAtPlayer = true;
    [SerializeField] private Transform player;
    [SerializeField] private CharacterController c;
    [SerializeField] private float hitCd = 2.0f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Animator animator;
    [SerializeField] private float maxHealth = 20000;
    [SerializeField] private RectTransform healthImg;
    [SerializeField] private GameObject winningCanvas;
    [Header("Sound")] 
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip rain;
    [SerializeField] private AudioClip bash;
    
    private float health;
    private float startingSize;

    [SerializeField] private float hitCooldown = 3.0f;
    private float hitAvailable;

    private Vector3 v3;

    private void Start()
    {
        startingSize = healthImg.transform.localScale.x;
        health = maxHealth;
        print(c.GetComponent<Collider>() + " \\ " + player.gameObject.GetComponent<CharacterController>().GetComponent<Collider>());
        Physics.IgnoreCollision(c.GetComponent<Collider>(), player.gameObject.GetComponent<CharacterController>().GetComponent<Collider>(), true);
        hitAvailable = hitCooldown;
        StartCoroutine(Behaviour());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInteractions>() != null && hitAvailable >= hitCooldown)
        {
            hitAvailable = 0.0f;
            PlayerInteractions.Instance.Damage();
        }
    }

    private void Update()
    {
        hitAvailable += Time.deltaTime;
        if (lookingAtPlayer)
        {
            transform.LookAt(player);
        }
    }

    private IEnumerator Behaviour()
    {
        LeanTween.moveY(gameObject, 4.19f, 6.0f);
        yield return new WaitForSeconds(6.0f);
        while (true)
        {
            StartCoroutine(SpiralShooting());
            yield return new WaitForSeconds(2.5f);
            StartCoroutine(Dash());
            yield return new WaitForSeconds(4.0f); 
        }
    }

    private IEnumerator SpiralShooting()
    {
        Vector3 position = muzzle.position;
        animator.SetTrigger("OpenEye");
        yield return new WaitForSeconds(0.5f);
        for (int i = 0, j = 0; i < 20; i++, j += 5)
        {
            source.PlayOneShot(rain);
            Instantiate(projectile, position, Quaternion.Euler(0, 0+j, 0));
            Instantiate(projectile, position, Quaternion.Euler(0, 60+j, 0));
            Instantiate(projectile, position, Quaternion.Euler(0, 120+j, 0));
            Instantiate(projectile, position, Quaternion.Euler(0, 180+j, 0));
            Instantiate(projectile, position, Quaternion.Euler(0, 240+j, 0));
            Instantiate(projectile, position, Quaternion.Euler(0, 300+j, 0));
            yield return new WaitForSeconds(0.2f);
        }
        animator.SetTrigger("CloseEye");
        
        yield return null;
    }
    private IEnumerator Dash()
    {
        for (int k = 0; k < 3; k++)
        {
            Vector3 v3 = player.position - transform.position;
            v3 = new Vector3(v3.x, 0, v3.z);
            Debug.Log(v3);
            animator.SetTrigger("OpenEye");
            lookingAtPlayer = false;
            for (int i = 0; i < 10; i++)
            {
                c.Move(-v3.normalized * 0.1f);
                transform.Rotate(new Vector3(20/16f,0, 0)); 
                yield return new WaitForSeconds(1f/16);
            }
        
            source.PlayOneShot(bash);
            for (int i = 0; i < 8; i++)
            {
                c.Move(v3.normalized * 1f);
                yield return new WaitForSeconds(0.1f/16);
            }
            animator.SetTrigger("CloseEye");
            lookingAtPlayer = true;
            yield return new WaitForSeconds(0.5f);
        }
        
        
    }

    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            winningCanvas.SetActive(true);
            Destroy(gameObject, 0.4f);
        }
        Transform ht = healthImg.transform;
        ht.localScale = new Vector3((health / maxHealth) * startingSize, ht.localScale.y, ht.localScale.z);
        Debug.Log((-(amount/maxHealth) * startingSize * 25.0f));
        Vector3 lp = new Vector3((-(amount/maxHealth) * startingSize * 50.0f), 0, 0);
        Debug.Log(lp);
        ht.localPosition += lp;
        
    }
}
