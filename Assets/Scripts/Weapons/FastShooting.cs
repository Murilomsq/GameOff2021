using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastShooting : MonoBehaviour, IEquippable
{
    [SerializeField] private float damage;
    [SerializeField] private float damageUpgrade;
    [SerializeField] private float speed;
    [SerializeField] private CharacterController c;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float fireRate = 0.2f;
    private float cdTimer = 0.0f;
    
    public void Equip()
    {
        this.enabled = true;
    }

    public void Unequip()
    {
        this.enabled = false;
    }

    public void UpgradeWeapon()
    {
        fireRate -= 0.015f;
        damage += damageUpgrade;
    }

    private void Update()
    {
        cdTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && cdTimer >= fireRate)
        {
            cdTimer = 0.0f;
            PlayerInteractions.Instance.generalAudioSource.PlayOneShot(PlayerInteractions.Instance.machineGunFire);
            BaseProjectile proj = Instantiate(projectile, muzzle.position, transform.rotation).GetComponent<BaseProjectile>();
            proj.damage = damage;
            proj.speed = speed;
        }
    }
}
