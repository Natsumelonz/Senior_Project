using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { set; get; }
    public AudioClip BGMMainMenu;
    public AudioClip BGMChapterSelect;
    public AudioClip BGMMatching;
    public AudioClip BGMScramble;
    public AudioClip BGMCh1;
    public AudioClip BGMCh2;
    public AudioSource BGMSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    void Start()
    {
        BGMSource.clip = BGMMainMenu;
        BGMSource.Play();
        BGMSource.loop = true;
    }
}
