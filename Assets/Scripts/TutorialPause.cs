using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class TutorialPause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;

    [Header("Audio")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer mixer;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("SavedMasterVolume", 100f);

        volumeSlider.value = savedVolume;
        ChangeVolume(savedVolume);

        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;

        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;

        LoadingManager.LoadScene("MainMenu");
    }

    public void ChangeVolume(float value)
    {
        if (value < 1f)
            value = 0.001f;

        PlayerPrefs.SetFloat("SavedMasterVolume", value);

        mixer.SetFloat("VolMaster", Mathf.Log10(value / 100f) * 20f);
    }
}
