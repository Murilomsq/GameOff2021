using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] private bool canHitPlayer;
    [SerializeField] private float speed = 10;
    [SerializeField] private float lifeTime = 10;
    [SerializeField] private float timeAfterFinished;
    [SerializeField] private ParticleSystem onTriggerParticles;
    private bool isBeeingDestroyed = false;
    [Header("Props")]
    [SerializeField] private float damage;

    public IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifeTime);
        if (onTriggerParticles != null)
        {
            onTriggerParticles.Play();
        }
        isBeeingDestroyed = true;
        Destroy(gameObject, timeAfterFinished);
    }

    public void DestroyProj()
    {
        if (onTriggerParticles != null)
        {
            onTriggerParticles.Play();
        }
        isBeeingDestroyed = true;
        Destroy(gameObject, timeAfterFinished);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("aa");
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable enemy))
        {
            enemy.Damage(damage);
        }

        if (other.gameObject.TryGetComponent<PlayerInteractions>(out PlayerInteractions player) && canHitPlayer)
        {
            player.Damage();
        }
        DestroyProj();
    }

    private void Start()
    {
        StartCoroutine(Lifetime());
    }

    private void FixedUpdate()
    {
        if(!isBeeingDestroyed)
            transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
    }
}
