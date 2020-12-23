using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPoint : MonoBehaviour
{
    internal int [] Contestors=new int[4];
    public int Capturecount;
    bool ready=true;
    bool isConquer;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Contestors.Length; i++)
        {
            Contestors[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isConquer && Capturecount > 0)
        {


            ready = true;
             StartCoroutine(Exit(Contestors[0]));
            
            
        }

       
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isConquer = true;
            if (ready)
            {


                Contestors[0] = 1;
                StartCoroutine(Conquer(Contestors[0]));
                ready = false;
            }
           
        }   
    }
    
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            

            isConquer = false;
            Contestors[0] = -1;
           
        }
    }
    
 
    IEnumerator Conquer(int Count)
    {
        Capturecount += Count;
        yield return new WaitForSeconds(1);
        ready = true;

    }  
    IEnumerator Exit(int Count)
    {
        yield return new WaitForSeconds(1);
        Capturecount += Count;
        ready = false;


    }
}
