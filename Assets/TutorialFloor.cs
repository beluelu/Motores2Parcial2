using UnityEngine;

public class TutorialFloor : MonoBehaviour
{
    public float speed = 6f;
    public bool canMove = true;

    void Update()
    {
        if (!canMove)
            return;

        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
