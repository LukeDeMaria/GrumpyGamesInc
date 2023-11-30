using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEye : MonoBehaviour
{
    public GameObject laserPrefab; 

    public float shootMinTime = 1.5f;
    public float shootMaxTime = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Shoot", shootMaxTime); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        Instantiate(laserPrefab, transform.position, laserPrefab.transform.rotation);
        float nextShotTime = Random.Range(shootMinTime, shootMaxTime);
        Invoke("Shoot", nextShotTime);
    }
}
