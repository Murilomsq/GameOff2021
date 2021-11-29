using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public List<GameObject> waves;
    [SerializeField] private List<GameObject> blockers;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.GetComponent<BoxCollider>());
        if (other.gameObject.TryGetComponent<PlayerInteractions>(out PlayerInteractions p))
        {
            foreach (GameObject blocker in blockers)
            {
                blocker.SetActive(true);
            }
            Debug.Log(waves[0]);
            waves[0].SetActive(true);
        }
    }
    
    public void NextWave()
    {
        if (waves.Count > 0)
        {
            waves[0].SetActive(true);
            return;
        }
        Destroy(gameObject, 0.4f);
    }

    private void End()
    {
        foreach (GameObject blocker in blockers)
        {
            blocker.SetActive(false);
        }
    }
    private void Start()
    {
        foreach (GameObject blocker in blockers)
        {
            blocker.SetActive(false);
        }
    }
}
