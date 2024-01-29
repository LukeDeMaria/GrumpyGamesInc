using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10;

    public HealthBar healthBar;

    public int maxHealth;
    public int currentHealth;

    public GameObject RocketPart;

    Transform target;
    NavMeshAgent agent; 

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform; 
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);


        if (distance <= lookRadius) 
        {
            agent.SetDestination(target.position);
        }

        if(currentHealth <= 0)
        {
            if (gameObject.tag == "Miniboss")
            {
                Instantiate(RocketPart, transform.position, transform.rotation);
            }
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
        currentHealth -= 1;
        healthBar.SetHealth(currentHealth);
    }

}
