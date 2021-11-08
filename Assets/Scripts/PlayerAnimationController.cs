using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform playerTransform;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float w1 = horizontal * Mathf.Cos((135 + playerTransform.rotation.eulerAngles.y) * Mathf.Deg2Rad) -
                   vertical * Mathf.Sin((135 + playerTransform.rotation.eulerAngles.y) * Mathf.Deg2Rad);
        float w2 = vertical * Mathf.Cos((135 + playerTransform.rotation.eulerAngles.y) * Mathf.Deg2Rad) +
            horizontal * Mathf.Sin((135 + playerTransform.rotation.eulerAngles.y) * Mathf.Deg2Rad);
        
        anim.SetFloat("Horizontal", w1);
        anim.SetFloat("Vertical", w2);
    }
}
