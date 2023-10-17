using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Vector3 velocity;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    float horizontalInput;
    float verticalInput;

    public float jumpHeight = 3f;

    bool hasDashed = false;
    public float dashForce;
    public float dashSpeed; 

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        hasDashed = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded == true && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        if (hasDashed == false)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        
        
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && hasDashed == false)
        {
            
            velocity.y += Mathf.Sqrt(dashForce * -2f * gravity);
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            hasDashed = true;
        }
    }

    
}
