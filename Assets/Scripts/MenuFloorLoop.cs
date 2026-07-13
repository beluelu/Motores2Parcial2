using UnityEngine;

public class MenuFloorLoop : MonoBehaviour
{
    public Transform otherFloor;

    public float speed = 8f;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z <= -33.9f)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                otherFloor.position.z + 69.8f
            );
        }
    }
}
