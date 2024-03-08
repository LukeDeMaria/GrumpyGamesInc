using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class ThirdPersonMovement : MonoBehaviour
{ 
    public CharacterController controller;
    //public GameObject enemySword;
    public GameObject astronautRig;
    public AudioSource audioSource;
    public AudioClip deathSound;
    public Transform cam;

    public GameObject respawnPnt;
    public GameObject player;

    public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;
    public GameObject dashBlue;
    public GameObject dashGray;
    TextMeshProUGUI rpText;

    public float speed;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Vector3 velocity;
    public float gravity = -9.81f;

    [HideInInspector]
    public float checkDistance = 0.4f;

    //[HideInInspector]
    public Transform check;
    [HideInInspector]
    public LayerMask groundMask;
    [HideInInspector]
    public LayerMask groundMask2;

    [HideInInspector]
    public LayerMask killzoneMask;

    [HideInInspector]
    public LayerMask hazardMask;

    [HideInInspector]
    public LayerMask mudMask;

    public LayerMask bouncyLowMask;
    public LayerMask bouncyMedMask;
    public LayerMask bouncyHighMask;

    [HideInInspector]
    public LayerMask poisonMask;

    public bool isGrounded;
    public bool isGrounded2;
    public bool touchingKillzone;
    public bool touchingHazard;
    public bool touchingMud;
    public bool touchingBouncyLow;
    public bool touchingBouncyMed;
    public bool touchingBouncyHigh;
    //public GameObject deathText;
    public bool touchingPoison;

    public bool isPoisoned;
    public bool takingChipDamage;


    public bool touchingRocketPart;
    public LayerMask rocketMask;


    public int rocketPartsHad = 0;
   // public int rocketPartsNeeded = 5;


    public bool hasDashed = false;

    public float iFrames = 1.0f;

    [HideInInspector]
    public float damageCooldown = 0;
    
    public float jumpHeight = 3f;
    public float dashHeight;
    public float dashSpeed;
    public float dashTime;
    public float horizontalInput;
    public float verticalInput;
    public BarrierDestroy barrierDestroy;
    Scene currentScene;
    
    [HideInInspector]
    Vector3 moveDir = new Vector3();


    void Start()
    {
        Animator anim = astronautRig.GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentScene = SceneManager.GetActiveScene();
        barrierDestroy = GameObject.Find("rocketCrashed").GetComponent<BarrierDestroy>();
        audioSource = gameObject.GetComponent<AudioSource>();
        rpText = GameObject.Find("RocketPartText").GetComponent<TextMeshProUGUI>();
        Cursor.lockState = CursorLockMode.Locked;
        rpText.text = "0/" + barrierDestroy.rocketPartsNeeded.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       
        Animator anim = astronautRig.GetComponent<Animator>();

        if (damageCooldown > 0)
        {
            damageCooldown -= Time.deltaTime;
        }
        isGrounded = Physics.CheckSphere(check.position, checkDistance, groundMask);
        isGrounded2 = Physics.CheckSphere(check.position, checkDistance, groundMask2);
        touchingKillzone = Physics.CheckSphere(check.position, checkDistance, killzoneMask);
        touchingHazard = Physics.CheckSphere(check.position, checkDistance, hazardMask);
        touchingMud = Physics.CheckSphere(check.position, checkDistance, mudMask);
        touchingBouncyLow = Physics.CheckSphere(check.position, checkDistance, bouncyLowMask);
        touchingBouncyMed = Physics.CheckSphere(check.position, checkDistance, bouncyMedMask);
        touchingBouncyHigh = Physics.CheckSphere(check.position, checkDistance, bouncyHighMask);
        touchingPoison = Physics.CheckSphere(check.position, checkDistance, poisonMask);
        Collider[] collectRocketParts = Physics.OverlapSphere(check.position, checkDistance, rocketMask);
        foreach (Collider rocketPart in collectRocketParts)
        {
            rocketPartsHad++;
            rpText.text = rocketPartsHad.ToString() + "/" + barrierDestroy.rocketPartsNeeded.ToString();
            rocketPart.GetComponent<RocketPartDestroy>().DestroyPart();
        }
        if (isGrounded == true || isGrounded2 == true)
        {
            //deathText.SetActive(false);

            hasDashed = false;
            dashBlue.SetActive(true);
            dashGray.SetActive(false);
            if(isGrounded == true)
            {
                isPoisoned = false;
                takingChipDamage = false;
            }
            if(isGrounded2 == true)
            {
                isPoisoned = true; 
                takingChipDamage = true;
            }

            anim.SetTrigger("IsOnGround");
            StopJumpAnimation();
        }
        else anim.ResetTrigger("IsOnGround");
        if (touchingKillzone == true)
        {
            TakeDamage(maxHealth);
        }
        if (touchingHazard == true)
        {
            TakeDamage(2);
            
        }
        if (touchingPoison == true)
        {
            isPoisoned = true;
            if(takingChipDamage == false)
            {
                StartCoroutine(PoisonChipDamage());
                takingChipDamage=true;
            }
            
        }

        if (touchingMud == true)
        {
            speed = 3;
        }
        else
        {
            speed = 13;
        }

        if (touchingBouncyLow == true)
        {
            velocity.y = Mathf.Sqrt((jumpHeight * 2f ) * -2f * gravity);
        }
        if (touchingBouncyHigh == true)
        {
            velocity.y = Mathf.Sqrt((jumpHeight * 7f) * -2f * gravity);
        }
        if (touchingBouncyMed == true)
        {
            velocity.y = Mathf.Sqrt((jumpHeight * 4f) * -2f * gravity);
        }

        if ((isGrounded && velocity.y < 0) || (isGrounded2 && velocity.y < 0))
        {
            velocity.y = -2f;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && (isGrounded == true || isGrounded2 == true))
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

       if((Input.GetButtonDown("Jump") && isGrounded) || (Input.GetButtonDown("Jump") && isGrounded2))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            JumpAnimation();

        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && hasDashed == false)
        {
            velocity.y = Mathf.Sqrt(dashHeight * -2f * gravity);
            StartCoroutine(Dash());
            hasDashed = true;
            anim.SetTrigger("IsDashing");
            StartDashAnim();
            dashBlue.SetActive(false);
            dashGray.SetActive(true);
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
           // audioSource.PlayOneShot(deathSound, 1);
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth <= 0)
        {
            Respawn();
            //deathText.SetActive(true);
            //SceneManager.LoadScene(currentScene.name);
            
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

    public void StartDashAnim()
    {
        Animator anim = astronautRig.GetComponent<Animator>();
        anim.ResetTrigger("IsDashing");
    }

    IEnumerator PoisonChipDamage()
    {
        takingChipDamage = true;
        yield return new WaitForSeconds(3);
        Debug.Log("Poison");
        TakeDamage(1);
        
        if (isPoisoned)
            StartCoroutine(PoisonChipDamage());
        }

    
    public void Respawn()
    {
        
        controller.enabled = false;
        player.transform.position = respawnPnt.transform.position;
        controller.enabled = true;
        currentHealth = 5;
        healthBar.SetHealth(currentHealth);
        Debug.Log("Dead!!!");
            Debug.Log("Player Position After Respawn: " + player.transform.position);
            Debug.Log("Rig Position After Respawn: " + astronautRig.transform.position);
    }
    }



