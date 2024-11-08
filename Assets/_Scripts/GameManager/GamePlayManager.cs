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
    public static event Action<float> OnUITime;
    
    [SerializeField] private GameObject flashLight;
    [SerializeField] private float maxEnergyFlashLight = 20f;
    [SerializeField] ManagerData managerData;
    
    public Light lightEnvironment;
    public float thunderMinTime = 6f; // Thời gian min
    public float thunderMaxTime = 15f; // Thời gian max
    public float flashDuration = 0.2f; // Thời gian sáng của tia chớp
    private bool isFlashing = false;

    public float speedFlashLight = 1f;
    private float countEnergyFlashLight = 0f;
    private bool flashLightOn = false;

    public float secondTimeMax = 300f;
    public float countSecondTime = 0f;
    private bool isGamePlay = true;
    public bool IsGamePlay {get {return isGamePlay;} set {isGamePlay = value;}}
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        countEnergyFlashLight = maxEnergyFlashLight;
        countSecondTime = secondTimeMax;
        
        StartCoroutine(CountDownTime());
        
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
            if (countEnergyFlashLight >= 0.3f && flashLightOn == false)
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
            countEnergyFlashLight -= speedFlashLight * Time.deltaTime;

            if (countEnergyFlashLight < 0.3f)
            {
                flashLightOn = false;
                if(flashLight.activeSelf) flashLight.SetActive(false);
            }
            
            UpdateUIFlashLight();
        }
    }

    IEnumerator CountDownTime()
    {
        while (isGamePlay)
        {
            yield return new WaitForSeconds(1f);
            countSecondTime--;
            if (countSecondTime < 0)
            {
                UIPlayer.instance.OnUIThatBai();
                ReCusor();
                isGamePlay = false;
            }
            else
            {
                UpdateUITime();
            }
        }
    }
    public bool AddEnergyFlashLight(float amount)
    {
        if (countEnergyFlashLight < maxEnergyFlashLight)
        {
            countEnergyFlashLight += amount / 100 * maxEnergyFlashLight;
        
            if (countEnergyFlashLight > maxEnergyFlashLight)
                countEnergyFlashLight = maxEnergyFlashLight;

            UpdateUIFlashLight();
            return true;
        }

        return false;
    }
    private IEnumerator WeatherCycle()
    {
        while (true)
        {
            float thunderWaitTime = Random.Range(thunderMinTime, thunderMaxTime);
            yield return new WaitForSeconds(thunderWaitTime);
            StartCoroutine(FlashLightning());
            yield return new WaitForSeconds(0.5f); // Đợi 0.5 giây trước khi có tiếng sấm
            AudioController.instance.OnAudioStorm();
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

    void UpdateUITime()
    {
        OnUITime?.Invoke(countSecondTime);
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