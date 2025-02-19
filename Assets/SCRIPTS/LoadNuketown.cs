using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNuketown : MonoBehaviour
{
    private string sceneName = "Nuketown";

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
