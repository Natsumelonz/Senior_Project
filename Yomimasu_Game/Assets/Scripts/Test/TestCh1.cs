using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using Proyecto26;

public class TestCh1 : MonoBehaviour
{
    private GameObject Manager;
    private int tIndex = 0;

    public Text question;
    public Text score;
    public List<Text> answerT = new List<Text>();
    public List<RetrieveTest> test = new List<RetrieveTest>();
    public List<AnswerChoice> answerChoice = new List<AnswerChoice>();
    public static TestCh1 main;
    public static bool pre;

    private void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        test = Manager.GetComponent<TestManager>().testCh1;

        NextQuestion();
        InitChoice();
    }

    private void Update()
    {
        if (pre)
        {
            score.text = "Your score: " + Manager.GetComponent<UserManager>().user.Pre[0];
        }
        else
        {
            score.text = "Your score: " + Manager.GetComponent<UserManager>().user.Post[0];
        }        
    }

    void Awake()
    {
        main = this;
    }

    public void NextQuestion()
    {
        if (tIndex < test.Count)
        {
            question.text = test[tIndex].test_desc;
            for (int i = 0; i < answerT.Count; i++)
            {
                answerT[i].text = test[tIndex].test_choice[i];
            }
        }
        else
        {
            if (pre)
            {
                Manager.GetComponent<UserManager>().user.PassPre[0] = true;
                Manager.GetComponent<UserManager>().Save();
                SceneManager.LoadScene("Chapter_1");
            }
            else
            {
                Manager.GetComponent<UserManager>().user.PassPost[0] = true;
                Manager.GetComponent<UserManager>().Save();
                SceneManager.LoadScene("Chapter");
            }
        }
    }

    public void CheckAnswer(AnswerChoice ac)
    {
        if (ac.index == test[tIndex].test_answer)
        {
            if (pre)
            {
                Manager.GetComponent<UserManager>().user.Pre[0] += 1;
            }
            else
            {
                Manager.GetComponent<UserManager>().user.Post[0] += 1;
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
        for (int i = 0; i < test[tIndex].test_choice.Count; i++)
        {
            answerChoice[i].Init(test[tIndex].test_choice[i], i);
        }
    }
}
