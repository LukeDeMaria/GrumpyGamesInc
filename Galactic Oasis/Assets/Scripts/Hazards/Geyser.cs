using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    public float x;
    public float y;
    public float z;
    bool canRise = true;
    void Start()
    {
        
    }

    void Update()
    {
        if (canRise) Wait();
    }

    IEnumerator Wait()
    {
        Debug.Log("Rising");
        canRise = false;
        x *= -1;
        y *= -1;
        z *= -1;
        transform.Translate(x, y, z);
        yield return new WaitForSeconds(4);
        x *= -1;
        y *= -1;
        z *= -1;
        transform.Translate(x, y, z);
        yield return new WaitForSeconds(4);
        canRise = true;
    }
}
