using UnityEngine;

public class CoinTutorialTrigger : MonoBehaviour
{
    public TopMessage topMessage;

    private void OnTriggerEnter(Collider other)
    {
        

        if (!other.CompareTag("Player"))
            return;

        

        topMessage.ShowMessage("¡Collect all the coins!");

        TutorialManager tutorial = FindFirstObjectByType<TutorialManager>();

        tutorial.waitingForTutorial = false;

        Destroy(gameObject);
    }
}
