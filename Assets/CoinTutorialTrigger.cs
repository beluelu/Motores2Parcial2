using UnityEngine;

public class CoinTutorialTrigger : MonoBehaviour
{
    public TopMessage topMessage;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (topMessage == null)
        {
            topMessage = FindFirstObjectByType<TopMessage>();
        }

        if (topMessage == null) return;

        topMessage.ShowMessage("¡Collect all the coins!");

        TutorialManager tutorial = FindFirstObjectByType<TutorialManager>();

        if (tutorial != null)
        {
            tutorial.waitingForTutorial = false;
        }

        Destroy(gameObject);
    }
}
