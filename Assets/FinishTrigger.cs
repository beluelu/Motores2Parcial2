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

        finishPanel.SetActive(true);

        Destroy(gameObject);
    }
}
