using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float lifeTime = 10;
    [SerializeField] private float timeAfterFinished;
    [SerializeField] private ParticleSystem onTriggerParticles;
    private bool isBeeingDestroyed = false;
    
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
