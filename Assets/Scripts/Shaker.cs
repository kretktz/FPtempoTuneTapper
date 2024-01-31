using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public IEnumerator ObjectShaker (float timer, float strength)
    {
        Vector3 originalPosition = transform.localPosition;

        float timePassed = 0.0f;

        while(timePassed < timer)
        {
            float xpos = Random.Range(-1f, 1f) * strength;
            float ypos = Random.Range(-1f, 1f) * strength;

            transform.localPosition = new Vector3(xpos, ypos, originalPosition.z);

            timePassed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
