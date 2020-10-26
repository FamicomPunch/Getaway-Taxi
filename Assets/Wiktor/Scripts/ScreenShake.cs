using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private float minRange = -0.1f;
    [SerializeField] private float maxRange = 0.1f;

    private Vector3 initialPos;
    public bool onShoulder = false;

    public IEnumerator Shake(float magnitude, float duration)
    {
        initialPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(minRange, maxRange) * magnitude;
            float y = Random.Range(minRange, maxRange) * magnitude;

            transform.localPosition = new Vector3(x, y, initialPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = initialPos;
    }

    public void EndShake()
    {
        transform.localPosition = initialPos;
        StopCoroutine(Shake(0,0));
    }
}
