using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using Proyecto26;

public class TestScene : MonoBehaviour
{
    private GameObject Manager;
    private int tIndex = 0;

    public Text question;
    public Text score;
    public List<Text> answerT = new List<Text>();
    public List<AnswerChoice> answerChoice = new List<AnswerChoice>();

    public static TestScene main;
    public static List<RetrieveTest> testhis;
    public static int sindex;

    private void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;

        NextQuestion();
        InitChoice();
    }

    private void Update()
    {
        if (!Manager.GetComponent<UserManager>().user.PassPre[sindex])
        {
            score.text = "Your score: " + Manager.GetComponent<UserManager>().user.Pre[sindex];
        }
        else
        {
            score.text = "Your score: " + Manager.GetComponent<UserManager>().user.Post[sindex];
        }        
    }

    void Awake()
    {
        main = this;
    }

    public void NextQuestion()
    {
        if (tIndex < testhis.Count)
        {
            question.text = testhis[tIndex].test_desc;
            for (int i = 0; i < answerT.Count; i++)
            {
                answerT[i].text = testhis[tIndex].test_choice[i];
            }
        }
        else
        {
            if (!Manager.GetComponent<UserManager>().user.PassPre[sindex])
            {
                Manager.GetComponent<UserManager>().user.PassPre[sindex] = true;
                Manager.GetComponent<UserManager>().Save();
                SceneManager.LoadScene("ChapterScene");
            }
            else
            {
                Manager.GetComponent<UserManager>().user.PassPost[sindex] = true;
                Manager.GetComponent<UserManager>().Save();
                SceneManager.LoadScene("Chapter");
            }
        }
    }

    public void CheckAnswer(AnswerChoice ac)
    {
        if (ac.index == testhis[tIndex].test_answer)
        {
            if (!Manager.GetComponent<UserManager>().user.PassPre[sindex])
            {
                Manager.GetComponent<UserManager>().user.Pre[sindex] += 1;
            }
            else
            {
                Manager.GetComponent<UserManager>().user.Post[sindex] += 1;
            }
            tIndex++;
            NextQuestion();
        }
        else
        {
            tIndex++;
            Debug.Log("Worng!");
            NextQuestion();
        }
    }

    public void InitChoice()
    {
        for (int i = 0; i < testhis[tIndex].test_choice.Count; i++)
        {
            answerChoice[i].Init(testhis[tIndex].test_choice[i], i);
        }
    }
}
