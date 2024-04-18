using System;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform target;

    public void Update()
    {
        Camera camera = Camera.main;
        transform.forward = Camera.main.transform.forward;
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);
    }
}