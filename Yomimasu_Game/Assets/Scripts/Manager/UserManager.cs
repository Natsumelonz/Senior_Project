using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Proyecto26;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

[Serializable]
public class User
{
    public string Name;
    public int LastCh;
    public int LastSentence;
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
        LoadUser();
    }

    public void NewUser()
    {
        user.Name = inputName.text;
        Debug.Log("Create new save name: " + user.Name);

        SaveUser();
    }

    public void SaveUser()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/user.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        if (user.Name != "")
        {
            RestClient.Put("https://it59-28yomimasu.firebaseio.com/User/" + user.Name + ".json", user);
        }

        //Debug.Log("Save path: " + path);
        formatter.Serialize(stream, user);
        stream.Close();

        LoadUser();
    }

    public void LoadUser()
    {
        string path = Application.persistentDataPath + "/user.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);


            User userData = formatter.Deserialize(stream) as User;
            stream.Close();

            fistTime = false;

            user = userData;
        }
        else
        {
            Debug.Log("Save not found!");
            panel.SetActive(true);
            fistTime = true;
        }
    }
}