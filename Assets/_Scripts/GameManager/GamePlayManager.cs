using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance;
    public static event Action<float, float> OnUIFlashLight;

    [SerializeField] private GameObject flashLight;
    [SerializeField] private float maxEnergyFlashLight = 20f;
    [SerializeField] private ManagerData managerData;

    private float countEnergyFlashLight = 0f;
    private bool flashLightOn = false;
    private InputDevice targetDevice;

    public XRNode controllerNode = XRNode.RightHand; // Chọn tay phải

    public XRDeviceSimulator simulator;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        countEnergyFlashLight = maxEnergyFlashLight;
        InitializeController();
    }

    private void InitializeController()
    {
        // Kiểm tra tay cầm thực tế
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);

        if (devices.Count > 0)
        {
            // Nếu có tay cầm thực tế, chọn thiết bị đầu tiên
            targetDevice = devices[0];
            Debug.Log("Using real VR controller");
        }
        else if (simulator != null)
        {
            // Nếu không có tay cầm thực tế nhưng có XR Device Simulator
            Debug.Log("Using XR Device Simulator");
        }
        else
        {
            // Nếu không có cả tay cầm thực tế và XR Device Simulator, sử dụng bàn phím
            Debug.Log("Using Keyboard input for flashlight");
        }
    }

    private void Update()
    {
        // Kiểm tra lại tay cầm thực tế hoặc simulator nếu không hợp lệ
        if (!targetDevice.isValid && simulator == null)
        {
            InitializeController();
        }

        // Kiểm tra nút bấm dựa trên loại thiết bị đã chọn
        if (targetDevice.isValid)
        {
            // Kiểm tra tay cầm thực tế
            if (targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool isPressed) && isPressed)
            {
                ToggleFlashlight();
            }
        }
        else if (simulator != null)
        {
            // Kiểm tra trigger của XR Device Simulator
            if (simulator.secondaryButtonAction.action.triggered)  // Kiểm tra tay phải
            {
                ToggleFlashlight();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            // Kiểm tra phím F trên bàn phím khi không có thiết bị VR
            ToggleFlashlight();
        }


        // Kiểm tra năng lượng đèn pin
        if (flashLightOn)
        {
            countEnergyFlashLight -= Time.deltaTime;

            if (countEnergyFlashLight < 1f)
            {
                flashLightOn = false;
                if (flashLight.activeSelf) flashLight.SetActive(false);
            }

            UpdateUIFlashLight();
        }
        else
        {
            // Tăng lại năng lượng đèn pin khi tắt
            countEnergyFlashLight += (1.5f * Time.deltaTime);

            if (countEnergyFlashLight > maxEnergyFlashLight)
                countEnergyFlashLight = maxEnergyFlashLight;

            UpdateUIFlashLight();
        }
    }

    private void ToggleFlashlight()
    {
        if (countEnergyFlashLight > 1f && !flashLightOn)
        {
            flashLightOn = true;
            if (!flashLight.activeSelf) flashLight.SetActive(true);
        }
        else
        {
            flashLightOn = false;
            if (flashLight.activeSelf) flashLight.SetActive(false);
        }
    }

    private void UpdateUIFlashLight()
    {
        OnUIFlashLight?.Invoke(countEnergyFlashLight, maxEnergyFlashLight);
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ChuyenMan()
    {
        int sceneNow = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        // Kiểm tra nếu scene hiện tại là scene cuối cùng
        if (sceneNow < totalScenes - 1)
        {
            if (managerData.ManChoiFinish < sceneNow + 1)
                managerData.ManChoiFinish = sceneNow + 1;
            SceneManager.LoadScene(sceneNow + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReCusor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
