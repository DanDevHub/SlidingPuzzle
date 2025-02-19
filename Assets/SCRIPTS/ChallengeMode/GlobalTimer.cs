using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GlobalTimer : MonoBehaviour
{
    public float timeForSolve = 1000f;
    public TextMeshProUGUI textTimer;
    public TextMeshProUGUI textTimer1;
    public TextMeshProUGUI textTimer2;
    public TextMeshProUGUI textTimer3;


    // Update is called once per frame
    void Update()
    {
        if(timeForSolve > 0){
            timeForSolve -= Time.deltaTime;
        }
        if(timeForSolve < 0){
            foreach (var obj in FindObjectsOfType<GameObject>())
                {                     
                    if (obj.scene.buildIndex == -1) // Objekte, die nicht zur Szene gehören
                    {
                        Destroy(obj);
                    }
                    }
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        if(timeForSolve < 11){
            textTimer.color = Color.red;
        }
            int min = Mathf.FloorToInt(timeForSolve / 60);
            int sec = Mathf.FloorToInt(timeForSolve % 60);
            textTimer.text = string.Format("{0}:{1:00}", min, sec);
            textTimer1.text = string.Format("{0}:{1:00}", min, sec);
            textTimer2.text = string.Format("{0}:{1:00}", min, sec);
            textTimer3.text = string.Format("{0}:{1:00}", min, sec);
    }
}
