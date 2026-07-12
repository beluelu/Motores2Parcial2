using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public Slider loadingSlider;

    IEnumerator Start()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(LoadingManager.sceneToLoad);

        while (!operation.isDone)
        {
            loadingSlider.value = operation.progress / 0.9f;
            yield return null;
        }
    }
}
