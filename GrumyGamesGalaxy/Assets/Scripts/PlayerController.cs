using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float jumpForce;

    public float forwardInput;
    public float sidewaysInput;

    public bool touchingGround = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checking for wasd input
        forwardInput = Input.GetAxis("Vertical");
        sidewaysInput = Input.GetAxis("Horizontal");
        // Move forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * sidewaysInput);
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * Time.deltaTime * jumpForce);
        }
    }
}
