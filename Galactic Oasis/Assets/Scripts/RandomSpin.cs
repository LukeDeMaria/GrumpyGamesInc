using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class RandomSpin : MonoBehaviour
{
    public float randomSpeed;
    public float randomRotate;
    // Start is called before the first frame update
    void Start()
    {
        randomRotate = Random.Range(1, 8);
        randomSpeed = Random.Range(100, 201);
    }

    // Update is called once per frame
    void Update()
    {
        // One rotation
        if (randomRotate == 1) transform.Rotate(randomSpeed * Time.deltaTime, 0, 0);
        if (randomRotate == 2) transform.Rotate(0, randomSpeed * Time.deltaTime, 0);
        if (randomRotate == 3) transform.Rotate(0, 0, randomSpeed * Time.deltaTime);

        // Two rotations
        if (randomRotate == 4) transform.Rotate(randomSpeed * Time.deltaTime, randomSpeed * Time.deltaTime, 0);
        if (randomRotate == 5) transform.Rotate(0, randomSpeed * Time.deltaTime, randomSpeed * Time.deltaTime);
        if (randomRotate == 6) transform.Rotate(randomSpeed * Time.deltaTime, 0, randomSpeed * Time.deltaTime);

        // Three rotations
        if (randomRotate == 7) transform.Rotate(randomSpeed * Time.deltaTime, randomSpeed * Time.deltaTime, randomSpeed * Time.deltaTime);
    }
}
