using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamage : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    public ThirdPersonMovement tpm;

    public float expandTime = 0.1f;
    public int damage = 1;


    // Start is called before the first frame update
    void Start()
    {
        tpm = GameObject.Find("Player").GetComponent<ThirdPersonMovement>();

        IncreaseAttackRange();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
        foreach (Collider player in hitPlayer)
        {
            tpm.TakeDamage(1);
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void IncreaseAttackRange()
    {
        attackRange += .51f;
        Invoke("IncreaseAttackRange", expandTime);
    }


}
