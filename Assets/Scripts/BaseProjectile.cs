using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float lifeTime = 10;
    
    public IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    
    public void Hit()
    {
        
    }

    private void Start()
    {
        StartCoroutine(Lifetime());
    }

    private void FixedUpdate()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
    }
}
