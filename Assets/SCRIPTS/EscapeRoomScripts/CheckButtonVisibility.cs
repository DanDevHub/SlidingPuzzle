using UnityEngine;

public class CheckButtonVisibility : MonoBehaviour
{
    public SocketChecker socket1Checker;  // Reference to the CheckObject script on the first socket
    public SocketChecker socket2Checker;  // Reference to the CheckObject script on the second socket
    public SocketChecker socket3Checker;  // Reference to the CheckObject script on the third socket
    public GameObject buttonToShow;     // Reference to the button GameObject that should appear

    public GameObject progress1;        // References to cylinders, which show progress
    public GameObject progress2;
    public GameObject progress3;

    public Texture activeTexture;        // Texture for "filled" state
    public Texture inactiveTexture;      // Texture for "empty" state

    //NPC-stuff
    public GameObject hamster1;
    public GameObject hamster2; // Assign in the Inspector
    public GameObject hamster3; // Assign in the Inspector
    public GameObject hamster4;
    public GameObject danielPuzzle;
    public GameObject antSpawner1;
    public GameObject antSpawner2;
    public GameObject hamsterTrap;

    private void Start()
    {
        // Ensure the button is initially hidden
        buttonToShow.SetActive(false);
    }

    private void LateUpdate()
    {
        // Calculate the total result from the sockets
        int socketCheck = socket1Checker.GetResult() +
                          socket2Checker.GetResult() +
                          socket3Checker.GetResult();

        // Update the progress colors based on the current result
        UpdateProgressColors(socketCheck);

        // Show the button only when all sockets are correctly filled
        buttonToShow.SetActive(socketCheck == 3);

        //NPC-stuff
        if (socketCheck ==3)
        {
            hamster1.gameObject.SetActive(false);
            hamster2.gameObject.SetActive(false); // Deactivate hamster2
            hamster3.gameObject.SetActive(true);  // Activate hamster3
            hamster4.gameObject.SetActive(true);
            danielPuzzle.gameObject.SetActive(true);   
            antSpawner1.gameObject.SetActive(false);
            antSpawner2.gameObject.SetActive(true);
            hamsterTrap.gameObject.SetActive(true);
        }
    }

    private void UpdateProgressColors(int socketCheck)
    {
        GameObject[] progressCylinders = { progress1, progress2, progress3 };

        for (int i = 0; i < progressCylinders.Length; i++)
        {
            MeshRenderer renderer = progressCylinders[i].GetComponent<MeshRenderer>();

            if (renderer != null)
            {
                renderer.material.mainTexture = (i < socketCheck) ? activeTexture : inactiveTexture;
            }
        }
    }
}
