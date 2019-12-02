using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public bool EnableFirstCamPosition;
    public Transform FirstCamPosition;
    public float smoothSpeed = 1.3f;
    public float mouseSensitivity = 10;
    public Transform target;
    public float dstFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    public bool canMove;
    float yaw;
    float pitch;

    public void Start()
    {
        canMove = true;
    }
    private void LateUpdate()
    {
        if (canMove == false)
            return;

        if (Input.GetKeyDown(KeyCode.Y) && EnableFirstCamPosition == true)
        {
            EnableFirstCamPosition = false;

        }

        if (EnableFirstCamPosition == true)
            return;
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputZ = Input.GetAxis("RightStickVertical");

        yaw += Input.GetAxis("Mouse X")  + inputX * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") + inputZ * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;
        transform.position = target.position - transform.forward * dstFromTarget;

        if (Input.GetKeyDown(KeyCode.Y))
        {
            EnableFirstCamPosition = true;
            MoveToFirstCamPosition();
        }
    }

    public void MoveToFirstCamPosition()
    {
        //  Vector3 desiredPositionn = Target.localPosition + AimOffset;
        Vector3 desiredPosition = FirstCamPosition.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

}
