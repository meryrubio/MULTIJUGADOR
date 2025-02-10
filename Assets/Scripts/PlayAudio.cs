using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioClip audioClip;
    public bool isLoop, playOnGameStart;
    public float volume;
    public string gameObjectName;


    // Start is called before the first frame update
    void Start()
    {
        if (playOnGameStart) //la musica suene nada mas empezar
        {
            AudioManager.instance.PlayAudio(audioClip, gameObjectName, isLoop, volume);
        }
    }
}
