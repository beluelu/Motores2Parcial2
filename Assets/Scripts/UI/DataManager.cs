using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [Header("Billetera Global")]
    public int currency;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("MonedasTotales", currency);
        PlayerPrefs.Save();
        Debug.Log("Monedas guardadas en el dispositivo: " + currency);
    }

    public void LoadData()
    {
        currency = PlayerPrefs.GetInt("MonedasTotales", 0);
    }

    public void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
        currency = 0;           
        SaveData();             
        Debug.Log("Todos los datos han sido borrados de PlayerPrefs.");
    }
}
