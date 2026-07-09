using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialManager tutorialManager;
    private bool activated = false;

    [TextArea]
    public string message;

    public TutorialManager.TutorialStep step;

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;
            tutorialManager.StopFloor(message, step);
        }
    }
}
