using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Slider loadingSlider;
    public TMP_Text loadingPercent;

    public float loadingTime = 5f; 

    IEnumerator Start()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(LoadingManager.sceneToLoad);

        
        operation.allowSceneActivation = false;

        float timer = 0f;

        while (timer < loadingTime)
        {
            timer += Time.deltaTime;

            float progress = Mathf.Clamp01(timer / loadingTime);

            loadingSlider.value = progress;
            loadingPercent.text = Mathf.RoundToInt(progress * 100f) + "%";

            yield return null;
        }

       
        loadingSlider.value = 1f;
        loadingPercent.text = "100%";

        
        operation.allowSceneActivation = true;
    }
}