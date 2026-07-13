using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class FinishPanelSound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (MusicFade.Instance != null)
        {
            MusicFade.Instance.LowerVolume();
        }

        audioSource.Play();

        StartCoroutine(RestoreMusic());
    }

    IEnumerator RestoreMusic()
    {
        yield return new WaitForSecondsRealtime(audioSource.clip.length);

        if (MusicFade.Instance != null)
        {
            MusicFade.Instance.RestoreVolume();
        }
    }
}
