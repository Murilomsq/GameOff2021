using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> waves;
    [SerializeField] private List<GameObject> blockers;
    [SerializeField] private int wave = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerInteractions>(out PlayerInteractions p))
        {
            foreach (GameObject blocker in blockers)
            {
                blocker.SetActive(true);
            }
            waves[wave].SetActive(true);
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
