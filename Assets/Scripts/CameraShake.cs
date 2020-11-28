using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
   public  IEnumerator ShakeRoutine (float duration , float magnitude)
    {
        Vector3 originalPos = transform.position;

        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, originalPos.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }
        
}
