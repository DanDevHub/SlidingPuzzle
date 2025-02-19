using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyTextController : MonoBehaviour
{
    public TextMeshProUGUI[] textFields;
    public Button nextButton;
    public Button resetButton;
    public Button spawnButton;  // New button
    public GameObject objectToDespawn;  // Object to be deactivated by spawnButton


    //hamster stuff
    public GameObject hamster9;
    public GameObject hamster10;
    public GameObject hamster11;
    public GameObject truckButton;

    private int currentIndex = 0;
    private bool hasCycledThroughAll = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize all text fields to be hidden except the first one
        for (int i = 0; i < textFields.Length; i++)
        {
            textFields[i].gameObject.SetActive(i == 0); //weil nur 0 true yano
        }


        // Hide the Reset Button initially
        resetButton.gameObject.SetActive(false);

        // Add a listener to the buttons
        nextButton.onClick.AddListener(ShowNextText);
        resetButton.onClick.AddListener(ResetTextCycle);
        spawnButton.onClick.AddListener(SpawnFirstTextAndDespawnObject);  // New listener
    }

    // Update is called once per frame
    void ShowNextText()
    {
        if (hasCycledThroughAll)
        {
            return; //do nothing
        }

        // Hide the current text field
        textFields[currentIndex].gameObject.SetActive(false);

        // Move to next field
        currentIndex = (currentIndex + 1) % textFields.Length; //weil dann wird wieder auf 0 reset..

        if (currentIndex == 0)
        {
            hasCycledThroughAll = true; //mark as cycled through
            HideAllTextFields();
            ShowResetButton();
            return;
        }

        // Show next text field
        textFields[currentIndex].gameObject.SetActive(true);
    }

    void HideAllTextFields()
    {
        // hide all text
        foreach (var textField in textFields)
        {
            textField.gameObject.SetActive(false);
        }
        // hide nextButton
        nextButton.gameObject.SetActive(false);
    }

    void ShowResetButton()
    {
        resetButton.gameObject.SetActive(true);
    }

    void ResetTextCycle()
    {
        //reset the cycle
        currentIndex = 0;
        hasCycledThroughAll = false;

        // show the first field
        textFields[currentIndex].gameObject.SetActive(true);

        //Hide the resetButton
        resetButton.gameObject.SetActive(false);

        //Show the nextButton
        nextButton.gameObject.SetActive(true);
    }

    // New method for spawnButton functionality
    void SpawnFirstTextAndDespawnObject()
    {
        // Show the first text field
        if (textFields.Length > 0)
        {
            textFields[0].gameObject.SetActive(true);
        }

        // Deactivate the specified GameObject
        if (objectToDespawn != null)
        {
            objectToDespawn.SetActive(false);
        }

        // Deactivate the spawn button itself
        spawnButton.gameObject.SetActive(false);
        hamster9.gameObject.SetActive(false);
        hamster10.gameObject.SetActive(false);
        hamster11.gameObject.SetActive(true);
        truckButton.gameObject.SetActive(true);

        //activate nextButton
        nextButton.gameObject.SetActive(true);
    }
}
