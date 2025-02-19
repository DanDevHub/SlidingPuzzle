using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textTimer;
    public float timeForSolve = 120f;
    private float remainingTime;
    public GameObject timerCanva;
    private bool isTimerRunning = false;
    public LevelManager levelManager;
    private SchiebePuzzleManager gameManager; 


    void Start(){
        gameManager = SchiebePuzzleManager.Instance;

    }
     private void OnEnable()
    {
        // Get the XR interactable component
        XRSimpleInteractable simpleInteractable = GetComponent<XRSimpleInteractable>();

        // Subscribe to the new selectEntered event with the correct handler signature
        simpleInteractable.selectEntered.AddListener(OnButtonPressed); // Event when the button is pressed
    }

    private void OnDisable()
    {
        // Get the XR interactable component again
        XRSimpleInteractable simpleInteractable = GetComponent<XRSimpleInteractable>();

        // Unsubscribe from the selectEntered event
        simpleInteractable.selectEntered.RemoveListener(OnButtonPressed);
    }

    // Updated method signature to accept SelectEnterEventArgs
    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        if (!isTimerRunning)
        {
            textTimer.color = Color.white;
            remainingTime = timeForSolve;
            timerCanva.SetActive(true);
            isTimerRunning = true;
            gameManager.spawnfirstPuzzle();
            //StartTimer();  
        }
    }


    private void StartTimer()
    {   
        isTimerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimerRunning == true){
            if(remainingTime > 0){
                remainingTime -= Time.deltaTime;
            }
            if(remainingTime < 0){
                isTimerRunning = false;
                GameObject prefabInstance = GameObject.Find("Puzzle");
                if (prefabInstance != null) //break when game finished
                {
                    levelManager = prefabInstance.GetComponent<LevelManager>();
                }
                if(levelManager != null){
                    levelManager.WinEnd(true);
                }
            }
            if(remainingTime < 11){
                textTimer.color = Color.red;
            }
            int min = Mathf.FloorToInt(remainingTime / 60);
            int sec = Mathf.FloorToInt(remainingTime % 60);
            textTimer.text = string.Format("{0}:{1:00}", min, sec);
        }  
    }
}
