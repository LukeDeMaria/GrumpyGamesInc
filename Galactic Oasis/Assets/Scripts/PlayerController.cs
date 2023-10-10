using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public float speed;
    public float jumpForce;
    public float diveForce;
    public float gravityModifier;

    public float forwardInput;
    public float sidewaysInput;

    public bool touchingGround = true;
    public bool hasDived = false;

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
        playerRb.position += forwardInput * transform.forward * Time.deltaTime * speed;
        playerRb.position += sidewaysInput * transform.right * Time.deltaTime * speed;
        if (Input.GetKeyDown(KeyCode.Space) && touchingGround)
         {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            touchingGround = false;
         }
        if (Input.GetKeyDown(KeyCode.LeftShift) && hasDived == false)
        {
            playerRb.AddForce(Vector3.up * diveForce, ForceMode.Impulse);
            touchingGround = false;
            hasDived = true;
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        touchingGround = true;
    }
}
