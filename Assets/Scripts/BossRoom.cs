using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField] private AudioClip BossFightSong;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerInteractions>(out PlayerInteractions p))
        {
            p.ost.Stop();
            p.ost.PlayOneShot(BossFightSong, 2f);
        }
    }
}
