using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterTrap : MonoBehaviour
{

    public GameObject hamster3;
    public GameObject hamster5;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            hamster3.gameObject.SetActive(false);
            hamster5.gameObject.SetActive(true);

        }
    }
}