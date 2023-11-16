using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject playerSword;
    public bool canAttack = true;
    public float attackCooldown = .5f;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public EnemyController enemyController; 

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canAttack)
            {
                SwordAttack();
            }
        }
    }

    public void SwordAttack()
    {
        //canAttack = false;
        Animator anim = playerSword.GetComponent<Animator>();
        anim.SetTrigger("PlayerAttack");

        SwordFunct();
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        //canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void SwordFunct()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<EnemyController>().TakeDamage(1);
        }

    }
}

