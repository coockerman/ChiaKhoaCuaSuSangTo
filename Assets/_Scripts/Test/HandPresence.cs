using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public bool showController = false; 
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;
    
    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;
    // Start is called before the first frame update
    void Start()
    {
        TryInitalize();
    }

    void TryInitalize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        //InputDevices.GetDevices(devices); //Lấy
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab != null)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.Log("No controller found");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
            
        }
        else
        {
            Debug.Log("No controller found");
            spawnedController = Instantiate(controllerPrefabs[0], transform);
        }
        spawnedHandModel = Instantiate(handModelPrefab, transform);
        handAnimator = spawnedHandModel.GetComponent<Animator>();
    }
    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0); 
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else 
        {
            handAnimator.SetFloat("Grip", 0); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitalize();
        }
        else
        {
            if (showController)
            {
                spawnedController.SetActive(true);
                spawnedHandModel.SetActive(false);
            }
            else
            {
                spawnedController.SetActive(false);
                spawnedHandModel.SetActive(true);
                UpdateHandAnimation();
            }
        }
        

        
        // if(targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue) 
        //     Debug.Log("Primary Button: " + primaryButtonValue);
        //
        // if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f) 
        //     Debug.Log("Trigger pressed: " + triggerValue);
        //
        // if(targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero) 
        //     Debug.Log("Primary touchpad: " + primary2DAxisValue);

    }
}
