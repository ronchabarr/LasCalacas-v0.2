using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
  public IEnumerator Shake(float duration,float magnitude)
    {
        Vector3 originPlace = transform.localPosition;

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector3(originPlace.x+x, originPlace.y+y, originPlace.z);

               elapsed += Time.deltaTime;
            yield return null;
        }


        transform.localPosition = originPlace;
    }
}
