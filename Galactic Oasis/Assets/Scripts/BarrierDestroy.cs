using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDestroy : MonoBehaviour
{

    public int rocketPartsNeeded = 5;
    public int rocketPartsHad = 0;


    void Start()
    {
        
    }

    void Update()
    {
        if(rocketPartsHad == rocketPartsNeeded)
        {
            Destroy(gameObject); 
        }
    }

   public void UpdateParts()
    {
        rocketPartsHad++; 
    }
}
