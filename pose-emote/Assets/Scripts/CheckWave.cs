using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class CheckWave : MonoBehaviour
{
    public XRNode leftHandNode = XRNode.LeftHand;
    public int positionHistoryLength = 10;
    public float waveThreshold = 0.15f;
    private List<Vector3> leftHandPositions = new List<Vector3>();

    public Image gameObjectImage;
    public Sprite handWaveImage;

    void Update()
    {
        // Get the left controller position
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(leftHandNode);
        if (leftHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftPosition))
        {
            // Store the position in history
            leftHandPositions.Add(leftPosition);
            if (leftHandPositions.Count > positionHistoryLength)
            {
                leftHandPositions.RemoveAt(0);
            }

            // Check if the hand is waving
            if (IsWaving())
            {
                gameObjectImage.sprite = handWaveImage;
            }
        }
    }

    bool IsWaving()
    {
        if (leftHandPositions.Count < positionHistoryLength)
        {
            return false;
        }

        float totalMovement = 0.0f;
        for (int i = 1; i < leftHandPositions.Count; i++)
        {
            totalMovement += Vector3.Distance(leftHandPositions[i - 1], leftHandPositions[i]);
        }

        return totalMovement > waveThreshold;
    }
}