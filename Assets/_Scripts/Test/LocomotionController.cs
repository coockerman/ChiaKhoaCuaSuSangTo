using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    public InputHelpers.Button teleportButton;
    public float activationThreshold = 0.1f;

    private void Update()
    {
        if (leftTeleportRay)
        {
            leftTeleportRay.gameObject.SetActive(CheckIfActiveted(leftTeleportRay));
        }
        if (rightTeleportRay)
        {
            rightTeleportRay.gameObject.SetActive(CheckIfActiveted(rightTeleportRay));
        }
    }

    public bool CheckIfActiveted(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportButton, out bool isPressed,activationThreshold);
        return isPressed;
    }
}
