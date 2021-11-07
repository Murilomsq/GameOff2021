using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform muzzle;
    [SerializeField] private ParticleSystem partSys;
    private ParticleSystem.MainModule settingsParticle;
    
    
    private IEnumerator ChargeRifle()
    {
        settingsParticle.startColor = new ParticleSystem.MinMaxGradient(new Color(0, 1, 0, 0.3f));
        yield return new WaitForSeconds(1.0f);
        settingsParticle.startColor = new ParticleSystem.MinMaxGradient(new Color(0,0,1, 0.3f));
        yield return new WaitForSeconds(1.0f);
        settingsParticle.startColor = new ParticleSystem.MinMaxGradient(new Color(1,0,0,0.3f));
    }

    private void Start()
    {
        settingsParticle = partSys.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            partSys.Play();
            StartCoroutine(ChargeRifle());
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
            partSys.Stop();
            Instantiate(projectile, muzzle.position, transform.rotation);
        }
    }
}
