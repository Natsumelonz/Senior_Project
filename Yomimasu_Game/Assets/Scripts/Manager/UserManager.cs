﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Proyecto26;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class User
{
    public string Name;
    public int LastCh;
    public int lastSentence;
    public int Score1;
    public int Score2;
    public int[] Pre = new int[10];
    public int[] Post = new int[10];
    public bool[] PassPre = new bool[10];
    public bool[] PassPost = new bool[10];
    public bool dictionary;
    public bool alphabetChart;
    public bool katakana;
    public List<int> LastScore1 = new List<int>();
    public List<int> LastScore2 = new List<int>();
}

public class UserManager : MonoBehaviour
{
    public static UserManager Instance { set; get; }
    public User user;
    public GameObject panel;
    public Text inputName;
    public static bool fistTime;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();

        Debug.Log(Helper.Serialize<User>(user));
    }

    public void Save()
    {
        PlayerPrefs.SetString("save", Helper.Serialize<User>(user));
        if (user.Name != "")
        {
            RestClient.Put("https://it59-28yomimasu.firebaseio.com/User/" + user.Name + ".json", user);
        }

        Debug.Log("Save Complete!");
        Load();
        fistTime = false;
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            user = Helper.Desrialize<User>(PlayerPrefs.GetString("save"));

            Debug.Log("Load Complete!");
        }
        else
        {
            panel.SetActive(true);
            fistTime = true;

            Debug.Log("No user found, create new one!");
        }
    }

    public void New()
    {
        user.Name = inputName.text;

        Debug.Log("Create new save name: " + user.Name);
    }
}