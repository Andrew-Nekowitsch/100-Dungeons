using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sound {

    public string audioName;
    public AudioClip audioClip;

    private AudioSource audioSource;

    public void setSource(AudioSource _source)
    {
        audioSource = _source;
        audioSource.clip = audioClip;
    }

}

public class AudioManager : MonoBehaviour
{
    
}
