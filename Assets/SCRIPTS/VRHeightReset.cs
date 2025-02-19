using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHeightReset : MonoBehaviour
{
    [SerializeField] private Transform cameraOffset;  // Assign "Camera Offset" GameObject
    [SerializeField] private Transform xrCamera;      // Assign XR Camera (Main Camera in VR)

    private float targetHeight = 1.18f; // Desired height in meters

    void Start()
    {
        if (cameraOffset == null || xrCamera == null)
        {
            Debug.LogError("CameraOffset or XRCamera not assigned. Please assign them in the Inspector.");
        }
    }

    public void ResetHeight()
    {
        if (cameraOffset == null || xrCamera == null) return;

        // Get the difference between current camera height and target height (1.2m)
        float heightDifference = targetHeight - xrCamera.localPosition.y;

        // Set the Camera Offset to correct the height difference
        cameraOffset.localPosition = new Vector3(
            cameraOffset.localPosition.x,
            heightDifference,  // Set offset Y directly instead of adding
            cameraOffset.localPosition.z
        );

        Debug.Log($"Height reset: Camera now at {xrCamera.localPosition.y}, Offset set to {cameraOffset.localPosition.y}");
    }
}
