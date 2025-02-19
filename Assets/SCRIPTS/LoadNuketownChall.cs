using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNuketownChall : MonoBehaviour
{
    private string sceneName = "Nuketown CHALLENGE";

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
