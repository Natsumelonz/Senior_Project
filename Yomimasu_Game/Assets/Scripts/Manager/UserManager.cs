using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Proyecto26;

[Serializable]
public class User
{
    public string Name;
    public int LastCh;
    public int Score1;
    public int Score2;
    public int[] Post = new int[10];
    public int[] Pre = new int[10];
}

public class UserManager : MonoBehaviour
{
    public static UserManager Instance { set; get; }
    public User user;

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

        Debug.Log("Save Complete!");
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
            user = new User();
            Save();
            Debug.Log("No user found, create new one!");
        }
    }
}