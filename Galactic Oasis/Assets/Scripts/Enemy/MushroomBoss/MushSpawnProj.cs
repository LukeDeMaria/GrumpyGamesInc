using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushSpawnProj : MonoBehaviour
{
    public GameObject projPrefab;

    public float minSpawnTime = 3.5f;
    public float maxSpawnTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", maxSpawnTime);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Vector3 spawnOffset = new Vector3(5, 0, 5);
        Instantiate(projPrefab, transform.position + spawnOffset, transform.rotation);
        float nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke("Spawn", nextSpawnTime);
    }
}
