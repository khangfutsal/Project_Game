using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapse = 0.0f;
        while(elapse < duration)
        {
            float x = UnityEngine.Random.Range(-1f, -1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, -1f) * magnitude;
            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapse += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
