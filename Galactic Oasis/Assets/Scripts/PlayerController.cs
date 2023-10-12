using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public float speed;
    public float rotateSpeed;
    public float dashSpeed;
    public float jumpForce;
    public float dashForce;
    public float gravityModifier;

    public float verticalInput;
    public float horizontalInput;

    public bool touchingGround = true;
    public bool hasDashed = false;

    //for camera
    Vector3 moveDirection;
    public Transform orientation;

    //ground check
    public LayerMask whatIsGround;
    bool grounded;
    public float groundDrag; 

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        //for the camera
        playerRb.freezeRotation = true; 
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, 0.7f, whatIsGround);
        //handleDrag
        if (grounded)
        {
            playerRb.drag = groundDrag;
        } else
        {
            playerRb.drag = 0; 
        }
        //camera
        MyInput();
        /*
        // Checking for wasd input
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        if (hasDashed == false)
        {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        
        // Move forward

            playerRb.position += verticalInput * transform.forward * Time.deltaTime * speed;
            playerRb.position += horizontalInput * transform.right * Time.deltaTime * speed;
        }
        if (Input.GetKeyDown(KeyCode.Space) && touchingGround)
         {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            touchingGround = false;
         }
        if (Input.GetKeyDown(KeyCode.LeftShift) && hasDashed == false)
        {
            playerRb.AddForce(Vector3.up * dashForce, ForceMode.Impulse);
            /*if (verticalInput == 0 || horizontalInput == 0)
            {
                playerRb.AddForce(transform.forward * (dashSpeed * 2), ForceMode.Impulse);
            }*/
            /*else
            {
                playerRb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
            //}
            touchingGround = false;
            hasDashed = true;
        }
        if (hasDashed == true && touchingGround == false)
        {
            verticalInput = 0;
            horizontalInput = 0;
            // Disable player input here
        }
        
        movementDirection.Normalize();
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
        */
    }

    private void MyInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void MovePlayer()
    {
        //calc move direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        playerRb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }
    private void OnCollisionEnter(Collision collision)
    {
        touchingGround = true;
        hasDashed = false;
        // Enable player input here
    }
}
