using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Seller : MonoBehaviour
{
    [SerializeField] private GameObject sellingCanvas;
    [SerializeField] private GameObject scalingCnvs;
    
    [SerializeField] private float duration = 1.0f;
    private Vector3 startingPos;

    public void DestroyCanvas()
    {
        LeanTween.scale(scalingCnvs, new Vector3(0, 0, 0), 0.5f).setEase(LeanTweenType.easeInElastic);
        Destroy(sellingCanvas, 0.5f);
        Destroy(gameObject, 0.5f);
    }
    
    #region Upgrades

    public void UpgradeSpeed()
    {
        PlayerInteractions.Instance.cm.speed += 1.0f;
    }

    public void UpgradeWeapon()
    {
        foreach (IEquippable weap in PlayerInteractions.Instance.weaponsEquipped)
        {
            weap.UpgradeWeapon();
        }
    }

    public void UpgradeDashCD()
    {
        PlayerInteractions.Instance.cm.dashCD -= 0.2f;
    }

    public void Heal()
    {
        PlayerInteractions.Instance.HealAll();
    }
    #endregion
    
    
    private IEnumerator Hover()
    {
        while (true)
        {
            LeanTween.moveLocalY(gameObject, startingPos.y + 0.25f , duration).setEase(LeanTweenType.easeInOutSine);
            
            yield return new WaitForSeconds(duration);
            LeanTween.moveLocalY(gameObject, startingPos.y - 0.25f , duration).setEase(LeanTweenType.easeInOutSine);
            
            yield return new WaitForSeconds(duration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sellingCanvas.SetActive(true);
            LeanTween.scale(scalingCnvs, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutElastic);
        }
    }
    private void Start()
    {
        startingPos = transform.position;
        StartCoroutine(Hover());
    }
}
