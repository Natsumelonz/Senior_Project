using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{    public static EffectManager Instance { set; get; }
    public AudioClip Click;
    public AudioSource EffectSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    void Start()
    {
        EffectSource.clip = Click;
    }
}
