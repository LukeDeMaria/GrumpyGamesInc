using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DELETE : MonoBehaviour
{
    public GameObject ws;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deleteThing()
    {
        Destroy(ws);
    }
}
