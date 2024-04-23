using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RocketFunc : MonoBehaviour
{

    public int rocketPartsNeeded = 5;
    public GameObject fixedRocket;
    public GameObject sceneLoad;
    public TextMeshProUGUI rpText;

    public ThirdPersonMovement tpm;
    
    void Update()
    {
        if(tpm.rocketPartsHad >= rocketPartsNeeded && tpm.enemyRocketPartGot)
        {
            rpText.faceColor = new Color(0f, 255f, 0f);
            Destroy(gameObject);
            fixedRocket.SetActive(true);
            sceneLoad.SetActive(true);
        }
    }
}
