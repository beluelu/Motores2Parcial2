using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public float speed = 10f;

    private bool canMove = false;

    private void Start()
    {
        transform.position = startPoint.position;
    }

    private void Update()
    {
        if (!canMove) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            endPoint.position,
            speed * Time.deltaTime
        );
    }

    public void StartTrain()
    {
        canMove = true;
    }
}
