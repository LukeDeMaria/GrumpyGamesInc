using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if(direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}
