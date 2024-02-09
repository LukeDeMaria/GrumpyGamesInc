using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAttack : MonoBehaviour
{
    public float attackMinTime = 1.5f;
    public float attackMaxTime = 5.5f;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    public ThirdPersonMovement tpm;
    // Start is called before the first frame update
    void Start()
    {
        tpm = GameObject.Find("Player").GetComponent<ThirdPersonMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        BugDamaging();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void BugDamaging()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (Collider player in hitPlayer)
        {
            Debug.Log("HIT!");
            tpm.TakeDamage(1);

        }
    }
}
