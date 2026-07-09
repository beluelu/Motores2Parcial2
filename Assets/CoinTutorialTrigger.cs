using UnityEngine;

public class CoinTutorialTrigger : MonoBehaviour
{
    public TopMessage topMessage;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Alguien entró al Trigger");

        if (!other.CompareTag("Player"))
            return;

        Debug.Log("Entró el Player");

        topMessage.ShowMessage("¡Recolecta todas las monedas!");

        TutorialManager tutorial = FindFirstObjectByType<TutorialManager>();

        tutorial.waitingForTutorial = false;

        Destroy(gameObject);
    }
}
