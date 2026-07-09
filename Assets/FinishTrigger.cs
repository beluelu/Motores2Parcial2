using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public GameObject finishPanel;
    public TutorialFloor tutorialFloor;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        tutorialFloor.canMove = false;

        PlayerController player = FindFirstObjectByType<PlayerController>();

        if (player != null)
        {
            player.enabled = false;
        }

        PlayerAnimation animation = FindFirstObjectByType<PlayerAnimation>();

        if (animation != null)
        {
            animation.StopRun();
        }

        finishPanel.SetActive(true);

        Destroy(gameObject);
    }
}
