using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSpinAttack : MonoBehaviour
{
    public GameObject player;
    public GameObject eye;
    public Transform attackPoint;
    public ThirdPersonMovement tpm;
    public float attackMinTime;
    public float attackMaxTime;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public LookAtPlayer lookAtPlayer;
    public UnityEngine.AI.NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        agent.updateRotation = false;
        Invoke("SpinAttack", attackMaxTime);
        lookAtPlayer = GetComponent<LookAtPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpinAttack()
    {
        lookAtPlayer.canFollow = false;
        Quaternion newRotation = eye.transform.rotation;
        eye.transform.rotation = Quaternion.RotateTowards(eye.transform.rotation, newRotation, Time.deltaTime * 180);

        //brackeys melee combat tutorial 
        EnemySpinFunct();

        float attackTime = Random.Range(attackMinTime, attackMaxTime);
        lookAtPlayer.canFollow = true;
        Invoke("SpinAttack", attackTime);
    }

    void EnemySpinFunct()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (Collider player in hitPlayer)
        {
            Debug.Log("HIT!");
            tpm.TakeDamage(2);

        }
    }
}
