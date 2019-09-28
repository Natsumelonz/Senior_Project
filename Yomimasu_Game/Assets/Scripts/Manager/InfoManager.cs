using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Proyecto26;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoManager : MonoBehaviour
{
    private GameObject Manager;
    private GameObject Audio;
    private GameObject Effect;

    public Text userName;
    public Text lastCh;
    public Text score1;
    public Text score2;
    public List<Text> preScore;
    public List<Text> postScore;


    private void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        Audio = GameObject.Find("AudioManager").gameObject;
        Effect = GameObject.Find("EffectManager").gameObject;

        userName.text = Manager.GetComponent<UserManager>().user.Name;
        lastCh.text = Manager.GetComponent<UserManager>().user.LastCh.ToString();
        score1.text = Manager.GetComponent<UserManager>().user.Score1 + " pt.";
        score2.text = Manager.GetComponent<UserManager>().user.Score2 + " pt.";

        for (int i = 0; i < Manager.GetComponent<UserManager>().user.Pre.Length; i++)
        {
            preScore[i].text = "Ch." + (i + 1) + ": " + Manager.GetComponent<UserManager>().user.Pre[i] + " pt.";
        }

        for (int i = 0; i < Manager.GetComponent<UserManager>().user.Post.Length; i++)
        {
            postScore[i].text = "Ch." + (i + 1) + ": " + Manager.GetComponent<UserManager>().user.Post[i] + " pt.";
        }
    }

    public void MainMenu()
    {
        Effect.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetInfo()
    {
        Effect.GetComponent<AudioSource>().Play();
        Manager.GetComponent<UserManager>().user.LastCh = 0;
        Manager.GetComponent<UserManager>().user.Score1 = 0;
        Manager.GetComponent<UserManager>().user.Score2 = 0;
        Manager.GetComponent<UserManager>().user.Pre = new int[10];
        Manager.GetComponent<UserManager>().user.Post = new int[10];
        Manager.GetComponent<UserManager>().user.PassPre = new bool[10];
        Manager.GetComponent<UserManager>().user.PassPost = new bool[10];
        Manager.GetComponent<UserManager>().user.LastScore1 = new List<int>();
        Manager.GetComponent<UserManager>().user.LastScore2 = new List<int>();
        Manager.GetComponent<UserManager>().Save();
        Manager.GetComponent<UserManager>().Load();

        SceneManager.LoadScene("MainMenu");
    }
}
