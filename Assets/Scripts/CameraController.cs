using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot; // 플레이어 자식인 Pivot 오브젝트
    [SerializeField] private Transform mainCamera;  // Pivot의 자식인 실제 카메라
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float maxVerticalAngle = 80f;
    [SerializeField] private float minVerticalAngle = -20f;

    private float rotationX = 0f; // 상하 각도 (Pivot의 localX)
    private float rotationY = 0f; // 좌우 각도 (Pivot의 localY)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 현재 localRotation에서 초기 각도 추출
        if (cameraPivot != null)
        {
            Vector3 currentRotation = cameraPivot.localEulerAngles;
            rotationY = currentRotation.y;
            rotationX = currentRotation.x;
            if (rotationX > 180) rotationX -= 360; // 0~360 범위를 -180~180으로 변환
        }
    }

    void Update()
    {
        if (cameraPivot == null) return;

        // 1. 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // 2. 회전 값 누적
        rotationY += mouseX;
        rotationX -= mouseY;

        // 3. 상하 회전 제한
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        // 4. 월드 좌표계 기준 회전 적용
        // 부모(플레이어)가 회전하더라도, Pivot의 월드 회전값을 직접 지정하면 부모의 회전 영향을 무시하게 됩니다.
        cameraPivot.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
