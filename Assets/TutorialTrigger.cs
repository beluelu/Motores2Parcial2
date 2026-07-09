using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialManager tutorialManager;

    [TextArea]
    public string message;

    public TutorialManager.TutorialStep step;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialManager.StopFloor(message, step);
        }
    }
}
