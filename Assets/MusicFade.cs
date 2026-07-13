using UnityEngine;
using System.Collections;

public class MusicFade : MonoBehaviour
{

    public static MusicFade Instance;

    private AudioSource music;

    private void Awake()
    {
        Instance = this;
        music = GetComponent<AudioSource>();
    }

    public void LowerVolume()
    {
        StopAllCoroutines();
        StartCoroutine(FadeTo(0.2f));
    }

    public void RestoreVolume()
    {
        StopAllCoroutines();
        StartCoroutine(FadeTo(1f));
    }

    IEnumerator FadeTo(float targetVolume)
    {
        while (Mathf.Abs(music.volume - targetVolume) > 0.01f)
        {
            music.volume = Mathf.Lerp(music.volume, targetVolume, Time.unscaledDeltaTime * 3f);
            yield return null;
        }

        music.volume = targetVolume;
    }
}
