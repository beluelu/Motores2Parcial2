using UnityEngine;

public class GameManager : MonoBehaviour
{
      public static GameManager instance;
    public int currentCoins = 0;
    public int coinsToWin = 20;

    public GameObject victoryPanel;

    [Header("Interfaz de Inicio Seguro")]
    [Tooltip("Arrastrá acá desde la jerarquía el cartel flotante que dice 'Tocar para empezar'")]
    public GameObject panelTouchToStart;

    private bool esperaToqueInicial = false;
    private PlayerAnimation playerAnim;

    [SerializeField] private bool startImmediately = false;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        playerAnim = FindFirstObjectByType<PlayerAnimation>();

        if (PlayerPrefs.HasKey("VengoDeAd") && PlayerPrefs.GetInt("VengoDeAd") == 1)
        {
            currentCoins = PlayerPrefs.GetInt("MonedasGuardadasAd", 0);
            Debug.Log("Monedas recuperadas con éxito tras el Ad: " + currentCoins);

            PlayerPrefs.SetInt("VengoDeAd", 0);
            PlayerPrefs.DeleteKey("MonedasGuardadasAd");
            PlayerPrefs.Save();
        }

        if (startImmediately)
        {
            ArrancarCarreraReal();
        }
        else
        {
            ActivarEsperaToque();
        }
    }

    private void Update()
    {
        if (esperaToqueInicial && Input.GetMouseButtonDown(0))
        {
            ArrancarCarreraReal();
        }
    }

    public void ActivarEsperaToque()
    {
        esperaToqueInicial = true;
        Time.timeScale = 0f;

        if (playerAnim != null) playerAnim.StopRun();
        if (panelTouchToStart != null) panelTouchToStart.SetActive(true);
    }

    private void ArrancarCarreraReal()
    {
        esperaToqueInicial = false;
        Time.timeScale = 1f;

        if (playerAnim != null) playerAnim.ResumeRun();
        if (panelTouchToStart != null) panelTouchToStart.SetActive(false);
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
