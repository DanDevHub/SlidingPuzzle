using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float timeForSolve = 300f;
    public float realTime;
    public TextMeshProUGUI textTimer;
    private SchiebePuzzleManager gameManager; 
    public bool isTimerRunning = false;
    public GameObject timerCanva;
    public GameManager horsePuzzle;

    void Start(){
        gameManager = SchiebePuzzleManager.Instance;
    }

    public void startRunning(){
        if(isTimerRunning == false){
            timerCanva.SetActive(true);
            isTimerRunning = true;
            realTime = timeForSolve;
        }
    }
    
    public void stopRunning(){
        isTimerRunning = false;
        timerCanva.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimerRunning == true){
            if(realTime > 0){
            realTime -= Time.deltaTime;
             }
            if(realTime < 0){
                gameManager.lifeDecrease();
                isTimerRunning = false;
                realTime = timeForSolve;
                if (horsePuzzle != null) //break when game finished
                {
                    horsePuzzle.Shuffle();
                }
             }
            if(realTime < 11){
                textTimer.color = Color.red;
            }
            int min = Mathf.FloorToInt(realTime / 60);
            int sec = Mathf.FloorToInt(realTime % 60);
            textTimer.text = string.Format("{0}:{1:00}", min, sec);
        }
        
    }
}
