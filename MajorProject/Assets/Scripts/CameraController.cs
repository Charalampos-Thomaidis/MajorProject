using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}