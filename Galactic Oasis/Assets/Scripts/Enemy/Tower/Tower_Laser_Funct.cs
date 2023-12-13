using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Laser_Funct : MonoBehaviour
{
    public float speed;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    private ThirdPersonMovement tpm;
    public int damage = 2;
    public float secondsTillDestroy = 2.2f;


    // Start is called before the first frame update
    void Start()
    {
        tpm = GameObject.Find("Player").GetComponent<ThirdPersonMovement>();
        Destroy(gameObject, secondsTillDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
        
        foreach (Collider player in hitPlayer)
        { 
            HitAndDestroy();
        }

    }

    void HitAndDestroy()
    {
        tpm.TakeDamage(damage);
        Destroy(gameObject); 
    }
}
