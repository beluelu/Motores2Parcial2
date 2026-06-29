using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;

    public bool hasShield = false;
    public int coinMultiplier = 1;

    private PlayerAnimation playerAnim;

    public UIHearts uiHearts;

    public int coins = 0;
    public UICoins uiCoins;

    [Header("Efectos de Sonido (Powerups y Monedas)")]
    public AudioSource audioSourceGlobal; 
    public AudioClip healSound;           
    public AudioClip doubleCoinsSound;

    void Start()
    {
        currentLives = maxLives;
        playerAnim = GetComponent<PlayerAnimation>();

        if (uiHearts != null)
            uiHearts.UpdateHearts(currentLives);

        if (uiCoins != null)
            uiCoins.UpdateCoins(coins);
    }

    public void TakeDamage(int amount)
    {
        if (hasShield)
        {
            Debug.Log("daño bloqueado");
            return;
        }

        currentLives -= amount;
        currentLives = Mathf.Clamp(currentLives, 0, maxLives);

        if (uiHearts != null)
            uiHearts.UpdateHearts(currentLives);

        if (currentLives <= 0)
        {
            if (playerAnim != null)
            {
                StartCoroutine(playerAnim.Fall());
            }
        }
    }

    public void Heal(int amount)
    {
        if (currentLives >= maxLives) return;

        currentLives += amount;
        currentLives = Mathf.Clamp(currentLives, 0, maxLives);

        if (audioSourceGlobal != null && healSound != null)
        {
            audioSourceGlobal.PlayOneShot(healSound);
        }

        if (uiHearts != null)
            uiHearts.UpdateHearts(currentLives);
    }

    public void ActivateShield(float duration)
    {
        StartCoroutine(ShieldCoroutine(duration));
    }

    IEnumerator ShieldCoroutine(float duration)
    {
        hasShield = true;
        yield return new WaitForSeconds(duration);
        hasShield = false;
    }

    public void ActivateDoubleCoins(float duration)
    {
        if (audioSourceGlobal != null && doubleCoinsSound != null)
        {
            audioSourceGlobal.PlayOneShot(doubleCoinsSound);
        }

        StartCoroutine(DoubleCoins(duration));
    }

    IEnumerator DoubleCoins(float duration)
    {
        coinMultiplier = 2;
        Debug.Log("¡Multiplicador x2 Activado en PlayerStats!");

        yield return new WaitForSeconds(duration);

        coinMultiplier = 1;
        Debug.Log("Multiplicador x2 Terminado.");
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        if (uiCoins != null)
            uiCoins.UpdateCoins(coins);
    }
}
