using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    [Header("Efecto de Sonido")]
    public AudioClip coinSound;

    void Update()
    {
        transform.Rotate(0, 150f * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();

            if (stats != null)
            {
                int total = value * stats.coinMultiplier;

                stats.AddCoins(total);

                if (stats.audioSourceGlobal != null && coinSound != null)
                {
                    stats.audioSourceGlobal.PlayOneShot(coinSound);
                }

                if (GameManager.instance != null)
                {
                    GameManager.instance.AddCoins(total);
                }
            }
            Destroy(gameObject);
        }
    }
}
