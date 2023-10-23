using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillzoneFunc : MonoBehaviour
{
    public ThirdPersonMovement playerHealth = new ThirdPersonMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        playerHealth.health = 0;
    }
}
