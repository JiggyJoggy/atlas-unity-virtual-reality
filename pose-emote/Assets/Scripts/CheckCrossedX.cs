using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class CheckCrossedX : MonoBehaviour
{
    public XRNode leftHandNode = XRNode.LeftHand;
    public XRNode rightHandNode = XRNode.RightHand;
    public float distanceThreshold = 0.15f;

    public Image gameObjectImage;
    public Sprite crossedXArmsImage;

    void Update()
    {
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(leftHandNode);
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(rightHandNode);

        if (leftHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftPosition) &&
            rightHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightPosition))
        {
            if (AreArmsCrossed(leftPosition, rightPosition))
            {
                gameObjectImage.sprite = crossedXArmsImage;
            }
        }
    }

    bool AreArmsCrossed(Vector3 leftPosition, Vector3 rightPosition)
    {
        // Calculate the distance between the controllers
        float distance = Vector3.Distance(leftPosition, rightPosition);

        // Check if the distance is within the threshold and if the controllers are crossed
        if (distance < distanceThreshold)
        {
            // Check if the left hand is to the right of the right hand and vice versa
            if (leftPosition.x > rightPosition.x && leftPosition.y < rightPosition.y)
            {
                return true;
            }
            if (leftPosition.x < rightPosition.x && leftPosition.y > rightPosition.y)
            {
                return true;
            }
        }

        return false;
    }
}
