using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject player;
    public GameObject respawnPnt;
    public ThirdPersonMovement tpm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(tpm.currentHealth == 0)
        {
            tpm.currentHealth = tpm.maxHealth;
            player.transform.position = respawnPnt.transform.position;
            Debug.Log("Dead!!!");
             
        } 
    }
}
