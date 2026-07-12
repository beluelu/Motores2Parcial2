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

    // --- ENTRADA SEGURA POST-ANUNCIO (PARCIAL MOTORES) ---
    private void Start()
    {
        // El juego nace limpio y revisa si en el disco quedó el aviso de que venimos de revivir por Ad
        if (PlayerPrefs.HasKey("VengoDeAd") && PlayerPrefs.GetInt("VengoDeAd") == 1)
        {
            // 1. Te devolvemos al marcador exactamente las monedas que habías juntado antes de morir
            currentCoins = PlayerPrefs.GetInt("MonedasGuardadasAd", 0);
            Debug.Log("Monedas recuperadas con éxito tras el Ad: " + currentCoins);

            // 2. Buscamos tus estadísticas de jugador para fijarte 1 sola vida en lugar de 3
            PlayerStats stats = FindFirstObjectByType<PlayerStats>();
            if (stats != null)
            {
                stats.currentLives = 1; // Arrancás la revancha con un solo corazón
                if (stats.uiHearts != null)
                {
                    stats.uiHearts.UpdateHearts(stats.currentLives); // Refresca tus corazones en pantalla
                }
            }

            // 3. Limpiamos los interruptores del disco para que el juego vuelva a la normalidad en la próxima partida
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
