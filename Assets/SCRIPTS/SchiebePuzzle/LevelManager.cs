using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LevelManager : MonoBehaviour
{
    public ParticleSystem winparticleSystem;
    public MovingElements moving;

    public void WinEnd(bool timeOver = false)
    {
        //winparticleSystem.Play();
        moving.Solved(this.gameObject, timeOver);
    }
}
