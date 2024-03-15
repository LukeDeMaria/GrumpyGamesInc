using System;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform target;
    public bool canFollow = true;

    void Update()
    {
        if (target != null && canFollow == true)
        {
            transform.LookAt(target);
        }
    }
}