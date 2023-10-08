using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public float speed;
    public float jumpForce;
    public float gravityModifier;

    public float forwardInput;
    public float sidewaysInput;

    public bool touchingGround = true;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // Checking for wasd input
        forwardInput = Input.GetAxis("Vertical");
        sidewaysInput = Input.GetAxis("Horizontal");
        // Move forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * sidewaysInput);
        if (Input.GetKeyDown(KeyCode.Space) && touchingGround)
         {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            touchingGround = false;
         }
    }

    private void OnCollisionEnter(Collision collision)
    {
        touchingGround = true;
    }
}
