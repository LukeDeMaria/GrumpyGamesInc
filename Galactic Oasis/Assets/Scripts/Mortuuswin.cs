using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortuuswin : MonoBehaviour
{
    public ThirdPersonMovement tpm;
    public RocketFunc rocket;
    public GameObject winScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tpm.enemiesKilled == tpm.enemiesToKill && tpm.rocketPartsHad >= rocket.rocketPartsNeeded)
        {
            winScreen.SetActive(true);
        }
    }
}
