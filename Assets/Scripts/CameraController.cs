using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerPivot;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float maxAngle = 45f;

    private float rotationX = 0f; // 상하 각도 누적
    private float rotationY = 0f; // 좌우 각도 누적

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // 회전 누적
        rotationY += mouseX;
        rotationX -= mouseY;

        // 상하, 좌우 회전 범위 제한
        rotationX = Mathf.Clamp(rotationX, -maxAngle, maxAngle);
        //rotationY = Mathf.Clamp(rotationY, -maxAngle, maxAngle);

        // 실제 회전 적용
        playerPivot.localRotation = Quaternion.Euler(0f, rotationY, 0f);
        cameraPivot.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
