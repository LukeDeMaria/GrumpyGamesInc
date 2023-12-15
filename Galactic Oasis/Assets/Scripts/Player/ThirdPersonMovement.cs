using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject enemySword;
    public GameObject astronautRig;
    public AudioSource audioSource;
    public AudioClip deathSound;
    public Transform cam;

    public int maxHealth = 8;
    public int currentHealth;
    public HealthBar healthBar;

    public float speed;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Vector3 velocity;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Transform killzoneCheck;
    public float killzoneDistance = 0.4f;
    public LayerMask killzoneMask;

    public bool isGrounded;
    public bool touchingKillzone;
    public bool hasDashed = false;
    
    public float jumpHeight = 3f;
    public float dashHeight;
    public float dashSpeed;
    public float dashTime;
    public float horizontalInput;
    public float verticalInput;
    public BarrierDestroy barrierDestroy;
    Scene currentScene;
    

    Vector3 moveDir = new Vector3();


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentScene = SceneManager.GetActiveScene();
        barrierDestroy = GameObject.Find("BarrierWall").GetComponent<BarrierDestroy>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        touchingKillzone = Physics.CheckSphere(killzoneCheck.position, killzoneDistance, killzoneMask);
        if (isGrounded == true)
        {
            hasDashed = false;
            StopJumpAnimation();
        }
        if (touchingKillzone == true)
        {
            TakeDamage(currentHealth);
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && isGrounded == true)
        {
            WalkAnimation();
        }
        else
        {
            StopWalkAnimation();
        }

        
        
        

        if (direction.magnitude >= 0.1f)
        {
            
            
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            
            controller.Move(moveDir.normalized * speed * Time.deltaTime); 
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            JumpAnimation();

        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && hasDashed == false)
        {
            velocity.y = Mathf.Sqrt(dashHeight * -2f * gravity);
            StartCoroutine(Dash());
            hasDashed = true;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    /*void OnCollisionEnter(Collision collision)
    {
        
    }*/

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime )
        {
            controller.Move(moveDir * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        audioSource.PlayOneShot(deathSound, 1);
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            
            SceneManager.LoadScene(currentScene.name);
        }
    }

    public void WalkAnimation()
    {
        Animator anim = astronautRig.GetComponent<Animator>();
        anim.SetTrigger("IsWalking");
    }

    public void StopWalkAnimation()
    {
        Animator anim = astronautRig.GetComponent<Animator>();
        anim.ResetTrigger("IsWalking");
    }

    public void JumpAnimation()
    {
        Animator anim = astronautRig.GetComponent<Animator>();
        anim.SetTrigger("IsJumping");
    }

    public void StopJumpAnimation()
    {
        Animator anim = astronautRig.GetComponent<Animator>();
        anim.ResetTrigger("IsJumping");
    }




}
