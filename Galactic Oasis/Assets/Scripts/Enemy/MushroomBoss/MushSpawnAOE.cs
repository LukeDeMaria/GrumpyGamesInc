using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushSpawnAOE : MonoBehaviour
{
    public float spawnTime = 20.0f;

    public GameObject aoePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Instantiate(aoePrefab, transform.position, transform.rotation);
        Invoke("Spawn", spawnTime);
    }
}
