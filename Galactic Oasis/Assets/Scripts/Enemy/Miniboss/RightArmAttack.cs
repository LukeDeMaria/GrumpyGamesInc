using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArmAttack : MonoBehaviour
{
    public GameObject rightArm;
    public GameObject player;
    public ThirdPersonMovement tpm;
    public float attackMinTime = 1.5f;
    public float attackMaxTime = 5.5f;
    public int damage = 1;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("RandomizeNextAttack", attackMaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomizeNextAttack()
    {
        int nextAttack = Random.Range(1, 3);
        Attack(nextAttack);
    }

    public void Attack(int attackToDo)
    {
        Animator anim = rightArm.GetComponent<Animator>();
        if(attackToDo == 1)
        {
            anim.SetTrigger("Swing");
            AttackFunct();

            float attackTime = Random.Range(attackMinTime, attackMaxTime);
            Invoke("RandomizeNextAttack", attackTime);
        } 
        else if(attackToDo == 2)
        {
            anim.SetTrigger("Jab");
            AttackFunct();

            float attackTime = Random.Range(attackMinTime, attackMaxTime);
            Invoke("RandomizeNextAttack", attackTime);
        }
    }

    public void AttackFunct()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (Collider player in hitPlayer)
        {
            Debug.Log("HIT!");
            tpm.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
