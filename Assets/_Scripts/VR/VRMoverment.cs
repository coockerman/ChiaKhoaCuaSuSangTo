using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRMoverment : MonoBehaviour
{
    public float speed = 2.0f;
    public XRNode inputSource; // Chọn tay cầm (LeftHand hoặc RightHand)
    private Vector2 inputAxis;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController component is required for VR movement.");
        }
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    void FixedUpdate()
    {
        // Lấy hướng dựa trên input từ cần gạt
        Vector3 direction = new Vector3(inputAxis.x, 0, inputAxis.y);
        direction = Camera.main.transform.TransformDirection(direction);
        direction.y = 0f; // Đảm bảo di chuyển không bị ảnh hưởng bởi độ nghiêng của camera

        // Di chuyển nhân vật
        characterController.Move(direction * speed * Time.fixedDeltaTime);
    }
}
