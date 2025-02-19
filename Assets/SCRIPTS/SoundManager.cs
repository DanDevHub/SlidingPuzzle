using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;



public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource; // Ziehe die AudioSource hier rein
    public Slider volumeSlider;     // Ziehe den UI-Slider hier rein

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = FindObjectOfType<AudioSource>();
        }

        if (volumeSlider == null)
        {
            volumeSlider = FindObjectOfType<Slider>();
        }
        

        // setzt lautstärke auf aktuellen Wert
        volumeSlider.value = audioSource.volume;

        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float value)
    {
        audioSource.volume = value;
    }

    
}
