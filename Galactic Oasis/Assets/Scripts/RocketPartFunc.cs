using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPartFunc : MonoBehaviour
{
    public Transform playerCheck;
    public float playerDistance = 0.4f;
    public LayerMask playerMask;

    public bool touchingPlayer;

    private BarrierDestroy barrierDestroy;

    void Start()
    {
        barrierDestroy = GameObject.Find("BarrierWall").GetComponent<BarrierDestroy>();
    }

    void Update()
    {
        touchingPlayer = Physics.CheckSphere(playerCheck.position, playerDistance, playerMask);
        if (touchingPlayer == true)
        {
            barrierDestroy.UpdateParts();
            Destroy(gameObject);
        }
    }

    public void Collect()
    {
        barrierDestroy.UpdateParts();
        Destroy(gameObject);
    }

    /*void OnCollisionEnter(Collision collision)
    {
        if (touchingPlayer == true)
        {
            barrierDestroy.UpdateParts();
            Destroy(gameObject);
        }
    }*/
}
