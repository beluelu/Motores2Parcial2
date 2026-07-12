using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentCoins = 0;
    public int coinsToWin = 20;

    public GameObject victoryPanel;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("VengoDeAd") && PlayerPrefs.GetInt("VengoDeAd") == 1)
        {
            currentCoins = PlayerPrefs.GetInt("MonedasGuardadasAd", 0);
            Debug.Log("Monedas recuperadas con éxito tras el Ad: " + currentCoins);

            PlayerPrefs.SetInt("VengoDeAd", 0);
            PlayerPrefs.DeleteKey("MonedasGuardadasAd");
            PlayerPrefs.Save();
        }
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        Debug.Log("Monedas actuales: " + currentCoins);

        if (currentCoins >= coinsToWin)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("GANASTE");

        if (DataManager.Instance != null)
        {
            DataManager.Instance.currency += currentCoins;
            DataManager.Instance.SaveData();
        }
        else
        {
            Debug.LogWarning("DataManager no encontrado. Entrá desde el menú de inicio.");
        }

        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
