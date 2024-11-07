using System;
using System.Collections;
using System.Collections.Generic;
using KinematicCharacterController.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance;
    public static event Action<float, float> OnUIFlashLight;
    
    [SerializeField] private GameObject flashLight;
    [SerializeField] private float maxEnergyFlashLight = 20f;
    [SerializeField] ManagerData managerData;
    [SerializeField] private Light lightEnvironment;
    
    public float thunderMinTime = 6f; // Thời gian min giữa các tiếng sấm
    public float thunderMaxTime = 15f; // Thời gian max giữa các tiếng sấm
    public float flashDuration = 0.2f; // Thời gian sáng chớp
    private bool isFlashing = false;
    
    private float countEnergyFlashLight = 0f;
    private bool flashLightOn = false;

    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        countEnergyFlashLight = maxEnergyFlashLight;
        if (lightEnvironment != null)
        {
            AudioController.instance.OnAudioWind();
            StartCoroutine(WeatherCycle());
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            ExamplePlayer.Instance.IsSelectMouse = false;
            UIPlayer.instance.OnSettingGame();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (countEnergyFlashLight > 1f && flashLightOn == false)
            {
                flashLightOn = true;
                if(!flashLight.activeSelf) flashLight.SetActive(true);
            }
            else
            {
                flashLightOn = false;
                if(flashLight.activeSelf) flashLight.SetActive(false);
            }
        }

        if (flashLightOn)
        {
            countEnergyFlashLight -= Time.deltaTime;

            if (countEnergyFlashLight < 1f)
            {
                flashLightOn = false;
                if(flashLight.activeSelf) flashLight.SetActive(false);
            }
            
            UpdateUIFlashLight();
        }
        else
        {
            countEnergyFlashLight += (1.5f * Time.deltaTime);
            
            if (countEnergyFlashLight > maxEnergyFlashLight)
                countEnergyFlashLight = maxEnergyFlashLight;

            UpdateUIFlashLight();
        }
    }

    private IEnumerator WeatherCycle()
    {
        while (true)
        {
            float thunderWaitTime = Random.Range(thunderMinTime, thunderMaxTime);
            yield return new WaitForSeconds(thunderWaitTime);
            AudioController.instance.OnAudioStorm();
            StartCoroutine(FlashLightning());
            
        }
    }
    private IEnumerator FlashLightning()
    {
        isFlashing = true;
        lightEnvironment.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        lightEnvironment.enabled = false;
        yield return new WaitForSeconds(flashDuration); // Chớp sáng lại
        lightEnvironment.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        lightEnvironment.enabled = false;
        isFlashing = false;
    }
    
    
    void UpdateUIFlashLight()
    {
        OnUIFlashLight?.Invoke(countEnergyFlashLight, maxEnergyFlashLight);
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ExamplePlayer.Instance.IsSelectMouse = true;
    }

    public void ChuyenMan()
    {
        int sceneNow = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        // Kiểm tra nếu scene hiện tại là scene cuối cùng
        if (sceneNow < totalScenes - 1)
        {
            if(managerData.ManChoiFinish < sceneNow + 1)
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
        ExamplePlayer.Instance.IsSelectMouse = false;
    }
}