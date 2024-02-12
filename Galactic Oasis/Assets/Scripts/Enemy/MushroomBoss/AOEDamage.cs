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


    // Start is called before the first frame update
    void Start()
    {
        IncreaseAttackRange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void IncreaseAttackRange()
    {
        attackRange += 1;
        Invoke("IncreaseAttackRange", expandTime);
    }


}
