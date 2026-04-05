using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;     
    public Vector3 offset;         
    public float followSpeed = 0.125f; 

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed);

        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}