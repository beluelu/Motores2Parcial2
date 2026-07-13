using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmationPanelControl : MonoBehaviour
{
    [Header("UI Objects")]
    [Tooltip("Drag and drop this same Panel object here")]
    public GameObject panelObject;

    public void OpenPanel()
    {
        if (panelObject != null)
        {
            panelObject.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        if (panelObject != null)
        {
            panelObject.SetActive(false);
        }
    }

    public void ConfirmDeleteAll()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.DeleteAllData();
        }

        ClosePanel();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
