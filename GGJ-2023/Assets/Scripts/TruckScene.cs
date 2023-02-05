using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckScene : MonoBehaviour
{
    public AudioSource truckSoundsPlayer;
    public List<AudioClip> truckSounds = new List<AudioClip>();
    
    void Start()
    {
        PlayStartup();
        Invoke("PlayContinous", truckSounds[0].length);
    }

    public void PlayStartup()
    {
        truckSoundsPlayer.clip = truckSounds[0];
        truckSoundsPlayer.Play();
    }

    public void PlayContinous()
    {
        truckSoundsPlayer.clip = truckSounds[1];
        truckSoundsPlayer.Play();
    }

    public void PlayStopping() 
    {
        truckSoundsPlayer.clip = truckSounds[2];
        truckSoundsPlayer.Play();
    }
}
