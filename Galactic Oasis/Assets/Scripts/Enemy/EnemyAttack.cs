using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject sword;
    public GameObject player;
    public float attackMinTime = 1.5f;
    public float attackMaxTime = 5.5f;

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
        float attackTime = Random.Range(attackMinTime, attackMaxTime);
        Invoke("SwordAttack", attackTime);
    }
}
