using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    //camerashake
    public float duration = 1f;
    public AnimationCurve curve;


    // Update is called once per frame
    void Update()
    {
        IEnumerator Shaking()
        {
            Vector3 startPosition = transform.position;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float strength = curve.Evaluate(elapsedTime / duration);
                transform.position = startPosition + Random.insideUnitSphere * strength;
                yield return null;
            }
            transform.position = startPosition;
        }

        //StartCoroutine(Shaking());

    }
}
