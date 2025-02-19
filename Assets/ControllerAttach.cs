using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI; // Required for UI

public class ControllerAttach : MonoBehaviour
{
    public GameObject map; // The 3D map that follows the controller
    public GameObject uiButton; // The UI Button that follows the controller
    public GameObject controller; // The VR Controller
    public InputActionProperty toggleMapAction; // Assigned in Inspector
    public InputActionProperty leftStickAction; // Left Stick X-axis input
    public ActionBasedContinuousMoveProvider moveProvider; // Reference to movement script
    public float sizeOffset = -0.4f; // Offset position for map

    private bool isMapActivated = false;
    public float rotationSpeed = 50f; // Adjust rotation speed

    private Quaternion additionalRotation = Quaternion.identity; // Stores extra rotation from joystick

    private void Start()
    {
        map.SetActive(isMapActivated);  // Ensure the map starts in the correct state
        uiButton.SetActive(isMapActivated); // Ensure the UI button starts in the correct state
    }

    void Update()
    {
        // Ensure input actions are valid
        if (toggleMapAction.action == null || leftStickAction.action == null)
        {
            Debug.LogWarning("Input actions are not assigned properly!");
            return;
        }

        // Toggle map and UI button visibility
        if (toggleMapAction.action.WasPressedThisFrame())
        {
            Debug.Log("Button Pressed");
            isMapActivated = !isMapActivated;
            map.SetActive(isMapActivated);
            uiButton.SetActive(isMapActivated); // Toggle UI button

            // Enable/Disable player movement when the map is toggled
            if (moveProvider != null)
            {
                moveProvider.enabled = !isMapActivated;
            }
        }

        if (isMapActivated)
        {
            // Keep the map attached to the controller in position
            map.transform.position = controller.transform.TransformPoint(new Vector3(0, sizeOffset, 0));

            // Keep the UI button directly attached to the controller
            uiButton.transform.position = controller.transform.TransformPoint(new Vector3(0, 0.10f, 0)); // Slightly offset above the map

            // Get Left Stick input for rotation
            float rotateInput = leftStickAction.action.ReadValue<Vector2>().x;

            if (Mathf.Abs(rotateInput) > 0.1f) // Prevents small unintended movements
            {
                additionalRotation *= Quaternion.Euler(0, rotateInput * rotationSpeed * Time.deltaTime, 0);
            }

            // Apply controller rotation + joystick rotation
            Quaternion initialRotation = Quaternion.Euler(0, -90, 0); // 90-degree rotation around the Y-axis
            Quaternion finalRotation = controller.transform.rotation * initialRotation * additionalRotation;

            map.transform.rotation = finalRotation;
            uiButton.transform.rotation = controller.transform.rotation; // Apply the same rotation to the UI button as the map
        }
    }

}
