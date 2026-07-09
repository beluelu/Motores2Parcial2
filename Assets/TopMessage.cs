using TMPro;
using System.Collections;
using UnityEngine;

public class TopMessage : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text text;

    public void ShowMessage(string message, float duration = 3f)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(message, duration));
    }

    IEnumerator ShowRoutine(string message, float duration)
    {
        panel.SetActive(true);
        text.text = message;

        yield return new WaitForSeconds(duration);

        panel.SetActive(false);
    }
}
