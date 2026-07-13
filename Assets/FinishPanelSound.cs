using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class FinishPanelSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        audioSource.Play();
    }
}
