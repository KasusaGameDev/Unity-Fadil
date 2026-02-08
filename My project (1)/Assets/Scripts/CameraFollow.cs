using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // GameObject yang diikuti
    public Vector3 offset;         // Jarak kamera dari target
    public float followSpeed = 5f; // Kecepatan mengikuti

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime
        );
    }
}
