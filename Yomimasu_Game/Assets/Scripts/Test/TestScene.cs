﻿using System.Collections;
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
    private int scoreP = 0;

    public Text question;
    public Text score;
    public List<Text> answerT = new List<Text>();
    public List<AnswerChoice> answerChoice = new List<AnswerChoice>();
    public GameObject pauseTab;
    public GameObject canvas;
    public GameObject panelFade;
    public Text fadeText;
    public GameObject event_image;

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
        score.text = "Your score: " + scoreP;
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
        event_image.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

        if (tIndex < testhis.Count)
        {
            event_image.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

            question.text = testhis[tIndex].test_desc;
            for (int i = 0; i < answerT.Count; i++)
            {
                answerT[i].text = testhis[tIndex].test_choice[i];
            }

            if (testhis[tIndex].test_sprite_path != null)
            {
                EventImageCall(Resources.Load<Sprite>(testhis[tIndex].test_sprite_path));
            }
        }
        else
        {
            if (!Manager.GetComponent<UserManager>().user.PassPre[sindex])
            {
                Manager.GetComponent<UserManager>().user.Pre[sindex] = scoreP;
                Manager.GetComponent<UserManager>().user.PassPre[sindex] = true;
                Manager.GetComponent<UserManager>().SaveUser();
                SceneManager.LoadScene("ChapterScene");
            }
            else
            {
                Manager.GetComponent<UserManager>().user.Post[sindex] = scoreP;
                Manager.GetComponent<UserManager>().user.PassPost[sindex] = true;
                Manager.GetComponent<UserManager>().SaveUser();
                SceneManager.LoadScene("Chapter");
            }
        }
    }
    private void EventImageCall(Sprite sprite_path)
    {
        event_image.GetComponent<Image>().sprite = sprite_path;
        event_image.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        event_image.GetComponent<Image>().preserveAspect = true;
    }

    public void CheckAnswer(AnswerChoice ac)
    {
        event_image.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

        if (ac.index == testhis[tIndex].test_answer)
        {
            scoreP += 1;
            tIndex++;
            NextQuestion();
        }
        else
        {
            Debug.Log("Worng!");
            tIndex++;
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
                if (!Manager.GetComponent<UserManager>().user.PassPre[sindex])
                {
                    Manager.GetComponent<UserManager>().user.Pre[sindex] = 0;
                }
                else
                {
                    Manager.GetComponent<UserManager>().user.Post[sindex] = 0;
                }
                break;
            case (1):
                SceneManager.LoadScene("Chapter");
                if (!Manager.GetComponent<UserManager>().user.PassPre[sindex])
                {
                    Manager.GetComponent<UserManager>().user.Pre[sindex] = 0;
                }
                else
                {
                    Manager.GetComponent<UserManager>().user.Post[sindex] = 0;
                }
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
