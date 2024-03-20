using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSpinAttack : MonoBehaviour
{
    public GameObject player;
    public Transform attackPoint;
    public ThirdPersonMovement tpm;
    public float attackTime = 6.0f;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public LookAtPlayer lookAtPlayer;
    public TowerEye towerEye;
    public UnityEngine.AI.NavMeshAgent agent;

    public int rotateSpeed;
    public float timer;


    // Start is called before the first frame update
    void Start()
    {
        agent.updateRotation = false;
        lookAtPlayer = GetComponent<LookAtPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5.99 && timer < 6.50) Spin();
        if (timer >= 8) StopSpin();
    }

    public void StopSpin()
    {
        timer = 0;
    }

    void Spin()
    {
        lookAtPlayer.enabled = false;
        towerEye.enabled = false;
        transform.Rotate(0, rotateSpeed, 0);
        lookAtPlayer.enabled = true;
        //towerEye.enabled = true;
    }
}
