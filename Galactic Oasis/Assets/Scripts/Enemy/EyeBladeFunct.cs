using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBladeFunct : MonoBehaviour
{
    public float speed;
    public Transform attackPoint;
    public float attackRange = 2f;
    public LayerMask playerLayer;
    public ThirdPersonMovement tpm;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
   /* void Update()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
        
        foreach (Collider player in hitPlayer)
        {
            tpm.TakeDamage(damage);
        }

    }*/

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") tpm.TakeDamage(2);
    }
}
