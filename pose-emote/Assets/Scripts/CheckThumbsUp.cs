using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class CheckThumbsUp : MonoBehaviour
{
    public XRNode leftHandNode = XRNode.LeftHand;

    public Image gameObjectImage;
    public Sprite thumbsUpImage;

    void Update()
    {
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(leftHandNode);

        if (IsThumbsUp(leftHand))
        {
            gameObjectImage.sprite = thumbsUpImage;
        }
    }

    bool IsThumbsUp(InputDevice device)
    {
        bool primaryButtonPressed = device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryValue) && primaryValue;
        bool secondaryButtonPressed = device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryValue) && secondaryValue;
        bool triggerPressed = device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue;
        bool gripPressed = device.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue) && gripValue;

        return !primaryButtonPressed && !secondaryButtonPressed && triggerPressed && !gripPressed;
    }
}
