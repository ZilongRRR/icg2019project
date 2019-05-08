using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookEntity : MonoBehaviour
{
    // Start is called before the first frame update
    bool checkCollision = false;

    public bool getCollision()
    {
        return checkCollision;
    } 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        checkCollision = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        checkCollision = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        checkCollision = false;
    }
}
