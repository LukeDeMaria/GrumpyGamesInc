using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEExpandMush : MonoBehaviour
{
    public Vector3 expandRate = new Vector3 (1, 1, 1);
    public float expandTime = 1.0f;
    public float secondsTillDestroy = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        Expand();
        Destroy(gameObject, secondsTillDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Expand()
    {
        transform.localScale += expandRate;
        Invoke("Expand", expandTime);
    }
}
