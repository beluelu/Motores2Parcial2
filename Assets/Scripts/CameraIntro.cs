using UnityEngine;
using System.Collections;

public class CameraIntro : MonoBehaviour
{
    public Transform endPosition;
    public float duration = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(Intro());
    }

    IEnumerator Intro()
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            transform.position = Vector3.Lerp(
                startPosition,
                endPosition.position,
                timer / duration);

            yield return null;
        }

        transform.position = endPosition.position;
    }
}
