using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomProjDestroy : MonoBehaviour
{
    public float secondsTillDestroy = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, secondsTillDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
