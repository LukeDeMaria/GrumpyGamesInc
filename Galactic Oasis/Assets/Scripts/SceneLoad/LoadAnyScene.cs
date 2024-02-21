using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadAnyScene : MonoBehaviour
{
    public Transform playerCheck;
    public float checkDistance;
    public LayerMask playerMask;
    public string SceneToLoad;
    public bool isColliding; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = Physics.CheckSphere(playerCheck.position, checkDistance, playerMask);
        if(isColliding )
        {
            SceneManager.LoadScene(SceneToLoad);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerCheck.position, checkDistance);
    }
}
