using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEndPoint : MonoBehaviour
{
    public LevelManager LevelManager;


    void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject.tag == "BoxPlayer"){
            LevelManager.WinEnd();
        }
    }
}
