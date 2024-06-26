using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10;
    public float iFrames = 0.75f;
    public int health = 1;
    public float distance;
    public float damageCooldown = 0;
    public ThirdPersonMovement tpm;
    public ParticleSystem explosion;

    public GameObject RocketPart;

    Transform target;
    NavMeshAgent agent;

    public HealthBar hpBar;

    // Start is called before the first frame update
    void Start()
    {
        tpm = GameObject.Find("Player").GetComponent<ThirdPersonMovement>();
        target = PlayerManager.instance.player.transform; 
        agent = GetComponent<NavMeshAgent>(); 

    }

    // Update is called once per frame
    void Update()
    {
        if (damageCooldown > 0)
        {
            damageCooldown -= Time.deltaTime;
        }
        distance = Vector3.Distance(target.position, transform.position);


        if (distance <= lookRadius) 
        {
            agent.SetDestination(target.position);
        }

        if(health <= 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            tpm.audioSource.PlayOneShot(tpm.soundFX[7], .1f);
            if (gameObject.tag == "Miniboss")
            {
               Instantiate(RocketPart, transform.position, transform.rotation);
            }
            tpm.enemiesKilled++;
            
            Destroy(gameObject); 
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius); 
    }

    public void TakeDamage(int damage)
    {
        tpm.audioSource.PlayOneShot(tpm.soundFX[4], .1f);
        if (damageCooldown <= 0)
        {
            health -= damage;
            hpBar.SetHealth(health);
        }
        damageCooldown = iFrames;
    }

}
