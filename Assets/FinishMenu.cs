using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishMenu : MonoBehaviour
{
    public void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
}
