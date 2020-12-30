using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_maxRange : MonoBehaviour
{
    //stats
    public bool isON {get; set;}

    void start()
    {
        isON = false;
    }
    void OnTriggerEnter(Collider collider)
    {
       isON = collider.gameObject.CompareTag("AI");
        
    }
    
}
