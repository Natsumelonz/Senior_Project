using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { set; get; }
    public AudioClip audioClip;
    public AudioSource mainMenuAudio;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    void Start()
    {
        mainMenuAudio.clip = audioClip;
        mainMenuAudio.Play();
    }

    void Update()
    {
        
    }
}
