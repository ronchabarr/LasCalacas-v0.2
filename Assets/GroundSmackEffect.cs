using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSmackEffect : MonoBehaviour
{
    

    public GameObject[] mainObj;
  public GameObject[] firstCircle;
  public GameObject[] secondCircle;
  public GameObject[] thirdCircle;
   
    public ParticleSystem[] Smokes;

    [SerializeField] float drawSpeed;
    [SerializeField] float remainTime;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] float offSet;
    [SerializeField] int upSpeed;
    [SerializeField] int downSpeed;

    public void Init()
    {
       
         


        foreach(GameObject found in mainObj)
        {
            found.SetActive(true);
        }
        StartCoroutine(MainRoutine());
       
    }
    IEnumerator MainRoutine()
    {
        foreach (GameObject found in firstCircle)
        {
           
            StartCoroutine(FirstCircleDraw(found,true));
            Smokes[0].Play();


        }
        yield return new WaitForSeconds(timeBetweenWaves);

        foreach (GameObject found in secondCircle)
        {
            
            StartCoroutine(FirstCircleDraw(found, true));
            Smokes[1].Play();


        }
        yield return new WaitForSeconds(timeBetweenWaves);
        foreach (GameObject found in thirdCircle)
        {
            StartCoroutine(FirstCircleDraw(found, true));
            Smokes[2].Play();


        }


        yield return new WaitForSeconds(1f);

        foreach (GameObject found in firstCircle)
        {

            StartCoroutine(FirstCircleDraw(found, false));


        }
        yield return new WaitForSeconds(timeBetweenWaves);

        foreach (GameObject found in secondCircle)
        {

            StartCoroutine(FirstCircleDraw(found, false));


        }
        yield return new WaitForSeconds(timeBetweenWaves);
        foreach (GameObject found in thirdCircle)
        {

            StartCoroutine(FirstCircleDraw(found, false));


        }
        yield return new WaitForSeconds(remainTime+offSet);

        foreach (GameObject found in mainObj)
        {
            found.SetActive(false);
        }
    }


    IEnumerator FirstCircleDraw(GameObject Rock ,bool isCasting)
    {
        if (isCasting)
        {
            for (int i = 0; i < upSpeed; i++)
            {
                Rock.transform.Translate(Vector3.left*2 / upSpeed);

                yield return new WaitForSeconds(drawSpeed / upSpeed);
            }
        }
        else
        {
            for (int i = 0; i < downSpeed; i++)
            {
                Rock.transform.Translate(Vector3.down * 1 /downSpeed);

                yield return new WaitForSeconds(remainTime / downSpeed);
            }
        }
      
     

      
    }
}
