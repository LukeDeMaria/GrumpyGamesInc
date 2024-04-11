using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject playerSword;
    public GameObject player;
    public ThirdPersonMovement tpm;
    public float attackCooldown;
    public float maxCooldown = 1.1f;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    

    void Start()
    {
        
    }

    void Update()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (attackCooldown <= 0)
            {
                SwordAttack();
            }
        }
    }

    public void SwordAttack()
    {
        tpm.audioSource.PlayOneShot(tpm.soundFX[2], 1);
        Animator anim = player.GetComponent<Animator>();
        anim.SetTrigger("IsAttacking");
    
        SwordFunct();
        attackCooldown = maxCooldown;
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

