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
    private GameObject Effect;
    private GameObject Audio;
    private int tIndex = 0;

    public Text question;
    public Text score;
    public List<Text> answerT = new List<Text>();
    public List<AnswerChoice> answerChoice = new List<AnswerChoice>();
    public GameObject pauseTab;
    public GameObject canvas;
    public GameObject panelFade;
    public Text fadeText;

    public static TestScene main;
    public static List<RetrieveTest> testhis;
    public static int sindex;

    private void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        Audio = GameObject.Find("AudioManager").gameObject;
        Effect = GameObject.Find("EffectManager").gameObject;

        StartCoroutine(FadeIn(3f));
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

    public IEnumerator FadeIn(float i)
    {
        panelFade.SetActive(true);
        canvas.SetActive(false);

        if (!Manager.GetComponent<UserManager>().user.PassPre[sindex])
        {
            fadeText.text = "Pre-Test";
        }
        else
        {
            fadeText.text = "Post-Test";
        }

        yield return new WaitForSeconds(i);

        panelFade.SetActive(false);
        canvas.SetActive(true);
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
                Manager.GetComponent<UserManager>().SaveUser();
                SceneManager.LoadScene("ChapterScene");
            }
            else
            {
                Manager.GetComponent<UserManager>().user.PassPost[sindex] = true;
                Manager.GetComponent<UserManager>().SaveUser();
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
    public void PauseButton()
    {
        Effect.GetComponent<AudioSource>().Play();
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseTab.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseTab.SetActive(false);
        }
    }

    public void PauseMenu(int i)
    {
        Time.timeScale = 1;
        switch (i)
        {
            default:
                break;
            case (0):
                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMMainMenu;
                Audio.GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("MainMenu");
                break;
            case (1):
                SceneManager.LoadScene("Chapter");
                break;
            case (2):
                Effect.GetComponent<AudioSource>().Play();
                if (Audio.GetComponent<AudioSource>().mute)
                {
                    Audio.GetComponent<AudioSource>().mute = false;
                }
                else
                {
                    Audio.GetComponent<AudioSource>().mute = true;
                }
                break;
        }
    }
}
