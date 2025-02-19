using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MultiTextController : MonoBehaviour
{
    public TextMeshProUGUI[] textFields;
    public Button nextButton;
    public Button resetButton;


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
}
