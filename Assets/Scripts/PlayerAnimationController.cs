using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform playerTransform;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal == 0 && vertical == 0)
        {
            anim.SetFloat("Horizontal", horizontal);
            anim.SetFloat("Vertical", vertical);
            return;
        }
        
        float angle = Mathf.Atan2(vertical,horizontal);
        horizontal = Mathf.Cos(angle)* Mathf.Abs(horizontal);
        vertical = Mathf.Sin(angle) * Mathf.Abs(vertical);

        

        float w1 = horizontal * Mathf.Cos((135 + playerTransform.rotation.eulerAngles.y) * Mathf.Deg2Rad) -
                   vertical * Mathf.Sin((135 + playerTransform.rotation.eulerAngles.y) * Mathf.Deg2Rad);
        float w2 = vertical * Mathf.Cos((135 + playerTransform.rotation.eulerAngles.y) * Mathf.Deg2Rad) +
            horizontal * Mathf.Sin((135 + playerTransform.rotation.eulerAngles.y) * Mathf.Deg2Rad);
        
        anim.SetFloat("Horizontal", w1);
        anim.SetFloat("Vertical", w2);
    }
}
