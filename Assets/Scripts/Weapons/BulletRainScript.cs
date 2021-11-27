using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletRainScript : MonoBehaviour, IEquippable
{
    [SerializeField] private GameObject bullet;
    [Header("fields")]
    [SerializeField] private int numOfProjectiles = 0;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private ParticleSystem partSys;
    private ParticleSystem.MainModule settingsParticle;

    public IEnumerator Charge()
    {
        for (int i = 0; i < 30; i++)
        {
            settingsParticle.startColor = new ParticleSystem.MinMaxGradient(new Color(i/30.0f, -(i/30.0f) + 1, 0, 1f));
            numOfProjectiles++;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public IEnumerator Shoot()
    {
        for (int i = 0; i < numOfProjectiles; i++)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + Random.Range(-15, 15), transform.rotation.eulerAngles.z));
            yield return new WaitForSeconds(fireRate);
        }
        numOfProjectiles = 0;
    }
    public void Equip()
    {
        this.enabled = true;
    }

    public void Unequip()
    {
        partSys.Stop();
        this.enabled = false;
    }
    private void Start()
    {
        settingsParticle = partSys.main;
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            partSys.Play();
            StartCoroutine(Charge());
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            partSys.Stop();
            StopAllCoroutines();
            StartCoroutine(Shoot());
        }
    }
}
