using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { set; get; }
    public AudioClip BGM;
    public AudioSource BGMSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    void Start()
    {
        BGMSource.clip = BGM;
        BGMSource.Play();
        BGMSource.loop = true;
    }
}
