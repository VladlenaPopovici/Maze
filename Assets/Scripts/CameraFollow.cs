using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 10, 0);
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (player == null)
            return;
        
        Vector3 desiredPosition = player.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
