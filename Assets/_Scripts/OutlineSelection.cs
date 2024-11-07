using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class OutlineSelection : MonoBehaviour
{
    private float khoangCachHover;
    [SerializeField] float defaultKhoangCach = 5f;
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    private Camera camera;

    public Transform controllerTransform; // Transform của tay cầm VR
    public InputDeviceCharacteristics controllerCharacteristics; // Đặc điểm của tay cầm VR

    private XRDeviceSimulator simulator;
    private InputDevice targetDevice; // Thiết bị VR thực tế

    void Start()
    {
        camera = Camera.main;
        khoangCachHover = defaultKhoangCach;
        simulator = FindObjectOfType<XRDeviceSimulator>();

        if (camera == null)
        {
            Debug.LogError("No camera found in the scene.");
        }
        if (simulator == null)
        {
            Debug.LogError("No XR Device Simulator found in the scene.");
        }
        if (controllerTransform == null)
        {
            Debug.LogError("No controller transform assigned.");
        }

        // Tìm kiếm InputDevice của tay cầm VR thực tế
        InitializeController();
    }

    void InitializeController()
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, inputDevices);
        
        if (inputDevices.Count > 0)
        {
            targetDevice = inputDevices[0];
        }
    }

    void Update()
    {
        // Disable previous highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        // Raycast từ tay cầm
        if (Physics.Raycast(controllerTransform.position, controllerTransform.forward, out raycastHit, khoangCachHover))
        {
            highlight = raycastHit.transform;
            if ((highlight.CompareTag("Selectable") || highlight.CompareTag("Item")) && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                }
            }
            else
            {
                highlight = null;
            }
        }

        // Kiểm tra nhấn trigger trong XR Device Simulator
        if (simulator != null && simulator.primaryButtonAction.action.triggered)
        {
            HandleInteraction();
        }
        // Kiểm tra nhấn trigger với thiết bị VR thực tế
        else if (targetDevice.isValid)
        {
            if (targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool isPressed) && isPressed)
            {
                HandleInteraction();
            }
        }
    }

    // Hàm xử lý khi nhấn nút trigger
    private void HandleInteraction()
    {
        if (highlight == null) return;

        if (highlight.CompareTag("Selectable"))
        {
            if (highlight.TryGetComponent<ActiveObject>(out ActiveObject rotationActive))
            {
                rotationActive.HanderAction();
            }
        }
        else if (highlight.CompareTag("Item"))
        {
            if (highlight.TryGetComponent<Item>(out Item itemGet))
            {
                itemGet.HanderAction();
            }
        }
    }
}
