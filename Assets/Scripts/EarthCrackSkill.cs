using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCrackSkill : MonoBehaviour
{

    [SerializeField] private GameObject[] leftWings;
    [SerializeField] private GameObject[] rightWings;
    [SerializeField] private GameObject[] mainObj;


    [SerializeField] float drawSpeed;
    [SerializeField] float remainTime;
    [SerializeField] float offSet;
    [SerializeField] int upSpeed;
    [SerializeField] int downSpeed;

    public void Init()
    {
        foreach (GameObject found in mainObj)
        {

        found.SetActive(true);
        }
        StartCoroutine(leftWingDraw());
        StartCoroutine(rightWingDraw());
     


    }
    IEnumerator leftWingDraw()
    {
        yield return new WaitForSeconds(offSet);
        GameObject PreviousRock = new GameObject();
        foreach (GameObject found in leftWings)
        {
            for (int i = 0; i < upSpeed; i++)
            {
                yield return new WaitForSeconds(drawSpeed / upSpeed);
                found.transform.Translate(Vector3.left / upSpeed);

            }
            float timeCounter = 0;
            if (PreviousRock != null)
            {


                timeCounter += remainTime;

                for (int i = 0; i < downSpeed; i++)
                {
                    yield return new WaitForSeconds(remainTime / downSpeed);
                    PreviousRock.transform.Translate(Vector3.right / downSpeed);

                }
            }
            PreviousRock = found;
            Debug.Log("1l");
            yield return new WaitForSeconds(drawSpeed - timeCounter);
        }

        for (int i = 0; i < downSpeed; i++)
        {
            yield return new WaitForSeconds(remainTime / downSpeed);
            PreviousRock.transform.Translate(Vector3.right / downSpeed);

        }
        foreach (GameObject found in mainObj)
        {

            found.SetActive(false);
        }


    }
    IEnumerator rightWingDraw()
    {
        GameObject PreviousRock = new GameObject();
        foreach (GameObject found in rightWings)
        {
            for (int i = 0; i < upSpeed; i++)
            {
                yield return new WaitForSeconds(drawSpeed / upSpeed);
                found.transform.Translate(Vector3.right / upSpeed);

            }

            float timeCounter = 0;
            if (PreviousRock != null)
            {


                timeCounter += remainTime;

                for (int i = 0; i < downSpeed; i++)
                {
                    yield return new WaitForSeconds(remainTime / downSpeed);
                    PreviousRock.transform.Translate(Vector3.left / downSpeed);
                }
            }
            PreviousRock = found;
            Debug.Log("1l");
            yield return new WaitForSeconds(drawSpeed - timeCounter);
        }
        for (int i = 0; i < downSpeed; i++)
        {
            yield return new WaitForSeconds(remainTime / downSpeed);
            PreviousRock.transform.Translate(Vector3.left / downSpeed);
        }
    }


}

