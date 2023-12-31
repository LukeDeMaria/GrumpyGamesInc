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

    public DashBar dashBar;

    public float speed;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Vector3 velocity;
    public float gravity = -9.81f;

    public float checkDistance = 0.4f;

    public Transform groundCheck;
    public LayerMask groundMask;

    public Transform killzoneCheck;
    public LayerMask killzoneMask;

    public Transform hazardCheck;
    public LayerMask hazardMask;

    public Transform mudCheck;
    public LayerMask mudMask;

    public LayerMask mushroomMask;

    public bool isGrounded;
    public bool touchingKillzone;
    public bool touchingHazard;
    public bool touchingMud;
    public bool touchingMushroom;


    public bool touchingRocketPart;
    public LayerMask rocketMask;


    public int rocketPartsHad = 0;
    public int rocketPartsNeeded = 5;


    public bool hasDashed = false;

    public float iFrames = 1.0f;
    public float damageCooldown = 0;
    
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

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (damageCooldown > 0)
        {
            damageCooldown -= Time.deltaTime;
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, checkDistance, groundMask);
        touchingKillzone = Physics.CheckSphere(killzoneCheck.position, checkDistance, killzoneMask);
        touchingHazard = Physics.CheckSphere(hazardCheck.position, checkDistance, hazardMask);
        touchingMud = Physics.CheckSphere(mudCheck.position, checkDistance, mudMask);
        touchingMushroom = Physics.CheckSphere(mudCheck.position, checkDistance, mushroomMask);
        Collider[] collectRocketParts = Physics.OverlapSphere(mudCheck.position, checkDistance, rocketMask);
        foreach (Collider rocketPart in collectRocketParts)
        {
            rocketPartsHad++;
            rocketPart.GetComponent<RocketPartDestroy>().DestroyPart();
        }
        if (isGrounded == true)
        {
            hasDashed = false;
            StopJumpAnimation();
        }
        if (touchingKillzone == true)
        {
            TakeDamage(currentHealth);
        }
        if (touchingHazard == true)
        {
            TakeDamage(2);
        }

        if (touchingMud == true)
        {
            speed = 5;
        }
        else
        {
            speed = 13;
        }

        if (touchingMushroom == true)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
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

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "RocketPart")
        {
            Destroy(collision.gameObject);

        }
    }


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
        if (damageCooldown <= 0)
        {
            currentHealth -= damage;
            audioSource.PlayOneShot(deathSound, 1);
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth <= 0)
        {
            
            SceneManager.LoadScene(currentScene.name);
        }
        damageCooldown = iFrames;
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
