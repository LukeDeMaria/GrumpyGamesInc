using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10;

    public int health = 1;
    public float distance;

    public GameObject RocketPart;

    Transform target;
    NavMeshAgent agent; 

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform; 
        agent = GetComponent<NavMeshAgent>(); 

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);


        if (distance <= lookRadius ) 
        {
            agent.SetDestination(target.position);
        }

        if(health <= 0)
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
        health -= damage;
    }

}
