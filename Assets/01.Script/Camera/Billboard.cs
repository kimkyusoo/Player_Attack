using UnityEngine;

// 빌보드 : 오브젝트가 카메라를 바라보도록 하는 기술
public class Billboard : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
    }
    private void LateUpdate()
    {
        if (mainCameraTransform == null) return;

        // UI가 카메라의 방향을 그대로 바라보게 합니다.
        // 카메라의 앞방향(forward) 벡터를 더해서 방향을 맞추는 것이 정석입니다.
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward, mainCameraTransform.rotation * Vector3.up);
    }
}
