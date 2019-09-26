using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{    public static EffectManager Instance { set; get; }
    public AudioClip Click;
    public AudioSource ClickSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    void Start()
    {
        ClickSource.clip = Click;
    }
}
