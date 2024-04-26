using System;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform target;

    public void Update()
    {
        transform.LookAt(target);
    }
}