using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEye : MonoBehaviour
{
    public GameObject laserPrefab;
    //public Transform target;
    public float shootMinTime = 3.5f;
    public float shootMaxTime = 5.5f;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("Shoot", shootMaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (target != null)
        {
            transform.LookAt(target);
        }
        */
    }

    void Shoot()
    {
            
            Instantiate(laserPrefab, transform.position, transform.rotation);
            float nextShotTime = Random.Range(shootMinTime, shootMaxTime);
            Invoke("Shoot", nextShotTime);

    }
}
