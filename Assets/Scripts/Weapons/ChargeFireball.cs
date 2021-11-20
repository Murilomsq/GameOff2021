using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeFireball : MonoBehaviour, IEquippable
{
    [SerializeField] private CharacterController c;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform muzzle;
    [SerializeField] private ParticleSystem partSys;
    private ParticleSystem.MainModule settingsParticle;
    private int shotPower = 0; 
    // This looks so damn unneficient, but I'll pretend im not the author of this
    private IEnumerator Knockback()
    {
        for (int i = 0; i < 5; i++)
        {
            c.Move(-transform.forward * 0.3f);
            yield return new WaitForSeconds(0.1f/5);
        }
    }
    private IEnumerator ChargeRifle()
    {
        shotPower = 0;
        settingsParticle.startColor = new ParticleSystem.MinMaxGradient(new Color(0, 1, 0, 0.3f));
        yield return new WaitForSeconds(1.0f);
        shotPower = 1;
        settingsParticle.startColor = new ParticleSystem.MinMaxGradient(new Color(0,0,1, 0.3f));
        yield return new WaitForSeconds(1.0f);
        shotPower = 2;
        settingsParticle.startColor = new ParticleSystem.MinMaxGradient(new Color(1,0,0,0.3f));
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
        if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
            partSys.Stop();
            switch (shotPower)
            {
                case 0:
                    break;
                case 1:
                    shotPower = 0;
                    break;
                case 2:
                    shotPower = 0;
                    Instantiate(projectile, muzzle.position, transform.rotation);
                    StartCoroutine(Knockback());
                    break;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            partSys.Play();
            StartCoroutine(ChargeRifle());
        }
    }
}
