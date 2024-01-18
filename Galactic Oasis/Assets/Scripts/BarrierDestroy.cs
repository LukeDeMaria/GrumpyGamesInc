using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDestroy : MonoBehaviour
{

    public int rocketPartsNeeded = 5;

    public ThirdPersonMovement tpm;

    void Start()
    {
        
    }
    
    void Update()
    {
        if(tpm.rocketPartsHad >= rocketPartsNeeded)
        {
            Destroy(gameObject); 
        }
    }
}
