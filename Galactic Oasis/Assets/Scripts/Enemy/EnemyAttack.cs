using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject sword;
    public GameObject player;
    public ThirdPersonMovement tpm;
    public float attackMinTime = 1.5f;
    public float attackMaxTime = 5.5f;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer; 

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SwordAttack", attackMaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwordAttack()
    {
        Animator anim = sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");

        //brackeys melee combat tutorial 
        EnemySwordFunct();

        float attackTime = Random.Range(attackMinTime, attackMaxTime);
        Invoke("SwordAttack", attackTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); 
    }

    void EnemySwordFunct()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (Collider player in hitPlayer)
        {
            Debug.Log("HIT!");
            tpm.TakeDamage(3);

        }
    }
}
