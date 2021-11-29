using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletRainScript : MonoBehaviour, IEquippable
{
    [SerializeField] private GameObject bullet;
    [Header("fields")]
    [SerializeField] private float bulletDmg;
    [SerializeField] private int numOfProjectiles = 0;
    [SerializeField] private int maxProjectiles = 40;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float chargeRate = 0.1f;
    [SerializeField] private ParticleSystem partSys;
    private ParticleSystem.MainModule settingsParticle;

    public IEnumerator Charge()
    {
        for (int i = 0; i < maxProjectiles; i++)
        {
            settingsParticle.startColor = new ParticleSystem.MinMaxGradient(new Color(i/30.0f, -(i/30.0f) + 1, 0, 1f));
            numOfProjectiles++;
            yield return new WaitForSeconds(chargeRate);
        }
    }
    public IEnumerator Shoot()
    {
        for (int i = 0; i < numOfProjectiles; i++)
        {
            GameObject go = Instantiate(bullet, transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + Random.Range(-15, 15), transform.rotation.eulerAngles.z));
            go.GetComponent<BaseProjectile>().damage = bulletDmg;
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

    public void UpgradeWeapon()
    {
        maxProjectiles += 5;
        fireRate -= 0.03f;
        chargeRate -= 0.02f;
    }


    private void Start()
    {
        settingsParticle = partSys.main;
        
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            partSys.Stop();
            StopAllCoroutines();
            StartCoroutine(Shoot());
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            numOfProjectiles = 0;
            partSys.Play();
            StartCoroutine(Charge());
        }
    }
}
