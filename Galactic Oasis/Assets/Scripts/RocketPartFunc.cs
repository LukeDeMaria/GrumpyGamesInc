using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPartFunc : MonoBehaviour
{

    private BarrierDestroy barrierDestroy;

    void Start()
    {
        barrierDestroy = GameObject.Find("BarrierWall").GetComponent<BarrierDestroy>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        barrierDestroy.UpdateParts();
        Destroy(gameObject);
    }
}
