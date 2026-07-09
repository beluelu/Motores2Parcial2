using UnityEngine;

public class TrainTrigger : MonoBehaviour
{
    public TrainMovement train;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        train.StartTrain();

        Destroy(gameObject);
    }
}
