using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuPause : MonoBehaviour
{
    [Header("Audio Mixer Reference")]
    public AudioMixer masterMixer;

    [Header("Slider UI")]
    public Slider soundSlider;

    private void Start()
    {

        if (masterMixer != null && soundSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("SavedMasterVolume", 100f);
            soundSlider.value = savedVolume;
            SetVolume(savedVolume);
        }
    }

    public void SetVolumeFromSlider()
    {
        if (soundSlider != null)
        {
            SetVolume(soundSlider.value);
        }
    }

    public void SetVolume(float value)
    {
        if (value < 1) value = 0.001f;


        PlayerPrefs.SetFloat("SavedMasterVolume", value);

        if (masterMixer != null)
        {
            masterMixer.SetFloat("VolMaster", Mathf.Log10(value / 100f) * 20f);
        }
    }
}
