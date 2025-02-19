using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketChecker : MonoBehaviour
{
    public GameObject Crystal;  // The GameObject you're expecting to be placed in the 

    // This is where you can store the result value (1 if the object is correct)
    public int result = 0;

    private void Start()
    {
        result = 0;
    }

    // Called when an object enters the socket
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Check if the object matches the Crystal GameObject
        if (args.interactableObject.transform.gameObject == Crystal)
        {
            result = 1; // Set the result to 1 if the correct object is placed in the socket
            Debug.Log("Correct object attached, result is 1");
        }
        else
        {
            result = 0; // Reset result if the object is incorrect
            Debug.Log("Incorrect object attached, result is 0");
        }
    }

    // Called when an object exits the socket
    public void OnSelectExited(SelectExitEventArgs args)
    {
        result = 0; // Reset result when the object is removed
        Debug.Log("Object removed, result is reset to 0");
    }

    // Optional: You can expose the result for other scripts or checks
    public int GetResult()
    {
        return result;
    }
}
