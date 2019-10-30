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
    public int Score1;
    public int Score2;
    public int[] LastIndex = new int[10];
    public int[] Pre = new int[10];
    public int[] Post = new int[10];
    public bool[] PassPre = new bool[10];
    public bool[] PassPost = new bool[10];
    public bool[] HiraganaChart = new bool[11];
    public bool[] KatakanaChart = new bool[11];
    public bool dictionary;
    public bool alphabetChart;
    public bool katakana;
    public List<int> LastScore1 = new List<int>();
    public List<int> LastScore2 = new List<int>();

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}

public class UserManager : MonoBehaviour
{
    public static UserManager Instance { set; get; }
    public User user = new User();
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

        RestClient.Get("https://it59-28yomimasu.firebaseio.com/User/" + user.Name + ".json").Then(response =>
        {
            if (response == null)
            {
                Debug.Log("User is null!");

                SaveUser();
            }
            else
            {
                Debug.Log("User not null!");

                RestClient.Get<User>("https://it59-28yomimasu.firebaseio.com/User/" + user.Name + ".json").Then(response1 =>
                {
                    user = response1;
                });

                SaveUser();
            }
        });
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

        Debug.Log("Save path: " + path);
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

            Debug.Log("Load path: " + path);
            User userData = formatter.Deserialize(stream) as User;
            stream.Close();

            fistTime = false;

            user = userData;

            RestClient.Get("https://it59-28yomimasu.firebaseio.com/User/" + user.Name + ".json").Then(response =>
            {
                if (response == null)
                {
                    Debug.Log("User is null!");
                }
                else
                {
                    Debug.Log("User not null!");

                    RestClient.Get<User>("https://it59-28yomimasu.firebaseio.com/User/" + user.Name + ".json").Then(response1 =>
                    {
                        user = response1;
                    });
                }
            });
        }
        else
        {
            Debug.Log("Save not found!");
            panel.SetActive(true);
            fistTime = true;
        }
    }
}