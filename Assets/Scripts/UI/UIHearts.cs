using UnityEngine;
using UnityEngine.UI;

public class UIHearts : MonoBehaviour
{
    public GameObject[] hearts;

    private PlayerStats playerStats;

    void Start()
    {
        
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }
    }

    void Update()
    {
        
        if (playerStats != null)
        {
            
            int vidasActuales = playerStats.currentLives;

            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].SetActive(i < vidasActuales);
            }
        }
    }

    
    public void UpdateHearts(int currentLives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentLives);
        }
    }
}
