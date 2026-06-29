using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class MainPanel : MonoBehaviour
{
    [Header("Global Wallet (UI)")]
    public TextMeshProUGUI globalCoinsText;

    [Header("Options")]
    public Slider volumeFX;
    public Slider volumeMaster;
    public Toggle mute;
    public AudioMixer mixer;
    public AudioSource fxSource;
    public AudioClip clickSound;
    private float lastVolume;

    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject levelSelectPanel;
    public GameObject shopPanel;

    private void Awake()
    {
        volumeFX.onValueChanged.AddListener(ChangeVolumeFX);
        volumeMaster.onValueChanged.AddListener(ChangeVolumeMaster);
    }

    private void Start()
    {
        UpdateCoinsText();
    }

    public void UpdateCoinsText()
    {
        if (DataManager.Instance != null && globalCoinsText != null)
        {
            globalCoinsText.text = DataManager.Instance.currency.ToString();
        }
    }

    public void DeleteGameProgress()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.DeleteAllData();
            UpdateCoinsText();
            PlaySoundButton();
            Debug.Log("Progreso eliminado y UI actualizada desde MainPanel.");
        }
    }

    public void PlayLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void SetMute()
    {
        if (mute.isOn)
        {
            mixer.GetFloat("VolMaster", out lastVolume);
            mixer.SetFloat("VolMaster", -80);
        }
        else
            mixer.SetFloat("VolMaster", lastVolume);
    }

    public void OpenPanel(GameObject panel)
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        shopPanel.SetActive(false);

        panel.SetActive(true);
        PlaySoundButton();
    }

    public void ChangeVolumeMaster(float v)
    {
        mixer.SetFloat("VolMaster", v);
    }

    public void ChangeVolumeFX(float v)
    {
        mixer.SetFloat("VolFX", v);
    }

    public void PlaySoundButton()
    {
        if (fxSource != null && clickSound != null)
        {
            fxSource.PlayOneShot(clickSound);
        }
    }

    public void QuitGame()
    {
        PlaySoundButton();
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
