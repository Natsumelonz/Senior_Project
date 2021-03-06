﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Proyecto26;
using UnityEngine.UI;

public class ChapterBehaviour : MonoBehaviour
{
    private GameObject Manager;
    private GameObject Audio;
    private GameObject Effect;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    private int panelPage = 0;

    public GameObject PreviousPanel;
    public Button alphabetChartButton;
    public AudioClip effectClip;
    public AudioSource effectAudio;
    public Button arrowLeft;
    public Button arrowRight;
    public List<GameObject> chapPanel;
    public List<GameObject> chapTextPanel;
    public List<GameObject> chapNTextPanel;
    public List<GameObject> chapOTextPanel;
    public List<GameObject> chapTTextPanel;

    private int chapterid;

    private void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        Audio = GameObject.Find("AudioManager").gameObject;
        Effect = GameObject.Find("EffectManager").gameObject;

        if (Manager.GetComponent<UserManager>().user.alphabetChart)
        {
            alphabetChartButton.interactable = true;
        }

        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen

        for (int i = 0; i < chapPanel.Count; i++)
        {
            chapTextPanel.Add(chapPanel[i].transform.Find("ChapText").gameObject);
            chapNTextPanel.Add(chapPanel[i].transform.Find("ChapNText").gameObject);
            chapOTextPanel.Add(chapPanel[i].transform.Find("ChapOText").gameObject);
            chapTTextPanel.Add(chapPanel[i].transform.Find("ChapTText").gameObject);
        }

        for (int i = 0; i < chapPanel.Count; i++)
        {
            chapTextPanel[i].GetComponent<Text>().text = "Chapter " + (i + 1);
            chapNTextPanel[i].GetComponent<Text>().text = Manager.GetComponent<ContentManager>().chapters_info[i].Name;
            chapOTextPanel[i].GetComponent<Text>().text = Manager.GetComponent<ContentManager>().chapters_info[i].Objective;
            chapTTextPanel[i].GetComponent<Text>().text = Manager.GetComponent<ContentManager>().chapters_info[i].chap_avg_time;
        }
    }
    public void Update()
    {
        SetProcess();
        Reposition();
        if (panelPage <= 0)
        {
            arrowLeft.interactable = false;
        }
        else
        {
            arrowLeft.interactable = true;
        }
        if (panelPage >= 7)
        {
            arrowRight.interactable = false;
        }
        else
        {
            arrowRight.interactable = true;
        }

        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            if (arrowLeft.interactable)
                            {
                                SwitchPanel(1);
                            }
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            if (arrowRight.interactable)
                            {
                                SwitchPanel(-1);
                            }
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }

    public void ClosePanel()
    {
        Effect.GetComponent<AudioSource>().Play();
        PreviousPanel.SetActive(false);
    }

    public void ChapterSelected(int i)
    {
        Effect.GetComponent<AudioSource>().Play();
        chapterid = i;
        ChapterScene.tindex = i - 1;
        TestScene.sindex = i - 1;

        switch (i)
        {
            default:
                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMMainMenu;
                Audio.GetComponent<AudioSource>().Play();

                SceneManager.LoadScene("MainMenu");
                break;
            case (-2):
                SceneManager.LoadScene("AlphabetsChart");
                break;
            case (0):
                break;
            case (1):
                SetChapter(i);
                ChapterPlay();
                break;
            case (2):
                if (Manager.GetComponent<UserManager>().user.LastCh < 1)
                {
                    PreviousPanel.SetActive(true);
                }
                else
                {
                    SetChapter(i);
                    ChapterPlay();
                }
                break;
            case (3):
                if (Manager.GetComponent<UserManager>().user.LastCh < 2)
                {
                    PreviousPanel.SetActive(true);
                }
                else
                {
                    SetChapter(i);
                    ChapterPlay();
                }
                break;
            case (4):
                if (Manager.GetComponent<UserManager>().user.LastCh < 3)
                {
                    PreviousPanel.SetActive(true);
                }
                else
                {
                    SetChapter(i);
                    ChapterPlay();
                }
                break;
            case (5):
                if (Manager.GetComponent<UserManager>().user.LastCh < 4)
                {
                    PreviousPanel.SetActive(true);
                }
                else
                {
                    SetChapter(i);
                    ChapterPlay();
                }
                break;
            case (6):
                if (Manager.GetComponent<UserManager>().user.LastCh < 5)
                {
                    PreviousPanel.SetActive(true);
                }
                else
                {
                    SetChapter(i);
                    ChapterPlay();
                }
                break;
            case (7):
                if (Manager.GetComponent<UserManager>().user.LastCh < 6)
                {
                    PreviousPanel.SetActive(true);
                }
                else
                {
                    SetChapter(i);
                    ChapterPlay();
                }
                break;
            case (8):
                if (Manager.GetComponent<UserManager>().user.LastCh < 7)
                {
                    PreviousPanel.SetActive(true);
                }
                else
                {
                    SetChapter(i);
                    ChapterPlay();
                }
                break;
        }
    }

    public void ChapterPlay()
    {
        Effect.GetComponent<AudioSource>().Play();

        switch (chapterid)
        {
            default:
                break;
            case (0):
                break;
            case (1):
                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMCh1;
                Audio.GetComponent<AudioSource>().Play();

                if (!Manager.GetComponent<UserManager>().user.PassPre[0])
                {
                    SceneManager.LoadScene("TestScene");
                }
                else
                {
                    SceneManager.LoadScene("ChapterScene");
                }
                break;
            case (2):
                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMCh2;
                Audio.GetComponent<AudioSource>().Play();

                if (!Manager.GetComponent<UserManager>().user.PassPre[1])
                {
                    SceneManager.LoadScene("TestScene");
                }
                else
                {
                    SceneManager.LoadScene("ChapterScene");
                }
                break;
            case (3):
                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMCh2;
                Audio.GetComponent<AudioSource>().Play();

                if (!Manager.GetComponent<UserManager>().user.PassPre[2])
                {
                    SceneManager.LoadScene("TestScene");
                }
                else
                {
                    SceneManager.LoadScene("ChapterScene");
                }
                break;
            case (4):
                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMCh2;
                Audio.GetComponent<AudioSource>().Play();

                if (!Manager.GetComponent<UserManager>().user.PassPre[3])
                {
                    SceneManager.LoadScene("TestScene");
                }
                else
                {
                    SceneManager.LoadScene("ChapterScene");
                }
                break;
            case (5):
                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMCh2;
                Audio.GetComponent<AudioSource>().Play();

                if (!Manager.GetComponent<UserManager>().user.PassPre[4])
                {
                    SceneManager.LoadScene("TestScene");
                }
                else
                {
                    SceneManager.LoadScene("ChapterScene");
                }
                break;
            case (6):
                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMCh2;
                Audio.GetComponent<AudioSource>().Play();

                if (!Manager.GetComponent<UserManager>().user.PassPre[5])
                {
                    SceneManager.LoadScene("TestScene");
                }
                else
                {
                    SceneManager.LoadScene("ChapterScene");
                }
                break;
            case (7):
                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMCh2;
                Audio.GetComponent<AudioSource>().Play();

                if (!Manager.GetComponent<UserManager>().user.PassPre[6])
                {
                    SceneManager.LoadScene("TestScene");
                }
                else
                {
                    SceneManager.LoadScene("ChapterScene");
                }
                break;
            case (8):
                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMCh2;
                Audio.GetComponent<AudioSource>().Play();

                if (!Manager.GetComponent<UserManager>().user.PassPre[7])
                {
                    SceneManager.LoadScene("TestScene");
                }
                else
                {
                    SceneManager.LoadScene("ChapterScene");
                }
                break;
        }
    }

    private void SetProcess()
    {
        for (int i = 0; i < Manager.GetComponent<ContentManager>().chapters_info.Length; i++)
        {
            chapPanel[i].GetComponentInChildren<Slider>().value = ((float)Manager.GetComponent<UserManager>().user.LastIndex[i]
                / (float)Manager.GetComponent<ContentManager>().chapters_info[i].Scripts.Count);
            chapPanel[i].GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text =
                string.Format("{0:0}", (((float)Manager.GetComponent<UserManager>().user.LastIndex[i]
                / (float)Manager.GetComponent<ContentManager>().chapters_info[i].Scripts.Count) * 100)) + "%";
        }
    }

    private void SetChapter(int i)
    {
        i -= 1;
        ChapterScene.dialogThis = Manager.GetComponent<ContentManager>().chapters_info[i].Scripts;
        ChapterScene.questionThis = Manager.GetComponent<ContentManager>().chapters_info[i].Questions;
        ChapterScene.chName = Manager.GetComponent<ContentManager>().chapters_info[i].Name;
        TestScene.testhis = Manager.GetComponent<ContentManager>().chapters_info[i].Tests;
    }

    public void SwitchPanel(int i)
    {
        if (panelPage > -1 && panelPage < 8)
        {
            panelPage -= i;
        }

        //Debug.Log(panelPage);
    }

    public void Reposition()
    {
        RectTransform transform0 = chapPanel[0].GetComponent<RectTransform>();
        RectTransform transform1 = chapPanel[1].GetComponent<RectTransform>();
        RectTransform transform2 = chapPanel[2].GetComponent<RectTransform>();
        RectTransform transform3 = chapPanel[3].GetComponent<RectTransform>();
        RectTransform transform4 = chapPanel[4].GetComponent<RectTransform>();
        RectTransform transform5 = chapPanel[5].GetComponent<RectTransform>();
        RectTransform transform6 = chapPanel[6].GetComponent<RectTransform>();
        RectTransform transform7 = chapPanel[7].GetComponent<RectTransform>();

        switch (panelPage)
        {
            default:
                break;
            case (0):
                transform0.anchoredPosition = Vector2.Lerp(transform0.anchoredPosition, new Vector2(0, 0), 5 * Time.deltaTime);
                transform1.anchoredPosition = Vector2.Lerp(transform1.anchoredPosition, new Vector2(1100f, 0), 5 * Time.deltaTime);
                transform2.anchoredPosition = Vector2.Lerp(transform2.anchoredPosition, new Vector2(2100f, 0), 5 * Time.deltaTime);
                transform3.anchoredPosition = Vector2.Lerp(transform3.anchoredPosition, new Vector2(3100f, 0), 5 * Time.deltaTime);
                transform4.anchoredPosition = Vector2.Lerp(transform4.anchoredPosition, new Vector2(4100f, 0), 5 * Time.deltaTime);
                transform5.anchoredPosition = Vector2.Lerp(transform5.anchoredPosition, new Vector2(5100f, 0), 5 * Time.deltaTime);
                transform6.anchoredPosition = Vector2.Lerp(transform6.anchoredPosition, new Vector2(6100f, 0), 5 * Time.deltaTime);
                transform7.anchoredPosition = Vector2.Lerp(transform7.anchoredPosition, new Vector2(7100f, 0), 5 * Time.deltaTime);
                break;
            case (1):
                transform0.anchoredPosition = Vector2.Lerp(transform0.anchoredPosition, new Vector2(-1100f, 0), 5 * Time.deltaTime);
                transform1.anchoredPosition = Vector2.Lerp(transform1.anchoredPosition, new Vector2(0f, 0), 5 * Time.deltaTime);
                transform2.anchoredPosition = Vector2.Lerp(transform2.anchoredPosition, new Vector2(1100f, 0), 5 * Time.deltaTime);
                transform3.anchoredPosition = Vector2.Lerp(transform3.anchoredPosition, new Vector2(2100f, 0), 5 * Time.deltaTime);
                transform4.anchoredPosition = Vector2.Lerp(transform4.anchoredPosition, new Vector2(3100f, 0), 5 * Time.deltaTime);
                transform5.anchoredPosition = Vector2.Lerp(transform5.anchoredPosition, new Vector2(4100f, 0), 5 * Time.deltaTime);
                transform6.anchoredPosition = Vector2.Lerp(transform6.anchoredPosition, new Vector2(5100f, 0), 5 * Time.deltaTime);
                transform7.anchoredPosition = Vector2.Lerp(transform7.anchoredPosition, new Vector2(6100f, 0), 5 * Time.deltaTime);
                break;
            case (2):
                transform0.anchoredPosition = Vector2.Lerp(transform0.anchoredPosition, new Vector2(-2100f, 0), 5 * Time.deltaTime);
                transform1.anchoredPosition = Vector2.Lerp(transform1.anchoredPosition, new Vector2(-1100f, 0), 5 * Time.deltaTime);
                transform2.anchoredPosition = Vector2.Lerp(transform2.anchoredPosition, new Vector2(0f, 0), 5 * Time.deltaTime);
                transform3.anchoredPosition = Vector2.Lerp(transform3.anchoredPosition, new Vector2(1100f, 0), 5 * Time.deltaTime);
                transform4.anchoredPosition = Vector2.Lerp(transform4.anchoredPosition, new Vector2(2100f, 0), 5 * Time.deltaTime);
                transform5.anchoredPosition = Vector2.Lerp(transform5.anchoredPosition, new Vector2(3100f, 0), 5 * Time.deltaTime);
                transform6.anchoredPosition = Vector2.Lerp(transform6.anchoredPosition, new Vector2(4100f, 0), 5 * Time.deltaTime);
                transform7.anchoredPosition = Vector2.Lerp(transform7.anchoredPosition, new Vector2(5100f, 0), 5 * Time.deltaTime);
                break;
            case (3):
                transform0.anchoredPosition = Vector2.Lerp(transform0.anchoredPosition, new Vector2(-3100f, 0), 5 * Time.deltaTime);
                transform1.anchoredPosition = Vector2.Lerp(transform1.anchoredPosition, new Vector2(-2100f, 0), 5 * Time.deltaTime);
                transform2.anchoredPosition = Vector2.Lerp(transform2.anchoredPosition, new Vector2(-1100f, 0), 5 * Time.deltaTime);
                transform3.anchoredPosition = Vector2.Lerp(transform3.anchoredPosition, new Vector2(0f, 0), 5 * Time.deltaTime);
                transform4.anchoredPosition = Vector2.Lerp(transform4.anchoredPosition, new Vector2(1100f, 0), 5 * Time.deltaTime);
                transform5.anchoredPosition = Vector2.Lerp(transform5.anchoredPosition, new Vector2(2100f, 0), 5 * Time.deltaTime);
                transform6.anchoredPosition = Vector2.Lerp(transform6.anchoredPosition, new Vector2(3100f, 0), 5 * Time.deltaTime);
                transform7.anchoredPosition = Vector2.Lerp(transform7.anchoredPosition, new Vector2(4100f, 0), 5 * Time.deltaTime);
                break;
            case (4):
                transform0.anchoredPosition = Vector2.Lerp(transform0.anchoredPosition, new Vector2(-4100f, 0), 5 * Time.deltaTime);
                transform1.anchoredPosition = Vector2.Lerp(transform1.anchoredPosition, new Vector2(-3100f, 0), 5 * Time.deltaTime);
                transform2.anchoredPosition = Vector2.Lerp(transform2.anchoredPosition, new Vector2(-2100f, 0), 5 * Time.deltaTime);
                transform3.anchoredPosition = Vector2.Lerp(transform3.anchoredPosition, new Vector2(-1100f, 0), 5 * Time.deltaTime);
                transform4.anchoredPosition = Vector2.Lerp(transform4.anchoredPosition, new Vector2(0f, 0), 5 * Time.deltaTime);
                transform5.anchoredPosition = Vector2.Lerp(transform5.anchoredPosition, new Vector2(1100f, 0), 5 * Time.deltaTime);
                transform6.anchoredPosition = Vector2.Lerp(transform6.anchoredPosition, new Vector2(2100f, 0), 5 * Time.deltaTime);
                transform7.anchoredPosition = Vector2.Lerp(transform7.anchoredPosition, new Vector2(3100f, 0), 5 * Time.deltaTime);
                break;
            case (5):
                transform0.anchoredPosition = Vector2.Lerp(transform0.anchoredPosition, new Vector2(-5100f, 0), 5 * Time.deltaTime);
                transform1.anchoredPosition = Vector2.Lerp(transform1.anchoredPosition, new Vector2(-4100f, 0), 5 * Time.deltaTime);
                transform2.anchoredPosition = Vector2.Lerp(transform2.anchoredPosition, new Vector2(-3100f, 0), 5 * Time.deltaTime);
                transform3.anchoredPosition = Vector2.Lerp(transform3.anchoredPosition, new Vector2(-2100f, 0), 5 * Time.deltaTime);
                transform4.anchoredPosition = Vector2.Lerp(transform4.anchoredPosition, new Vector2(-1100f, 0), 5 * Time.deltaTime);
                transform5.anchoredPosition = Vector2.Lerp(transform5.anchoredPosition, new Vector2(0f, 0), 5 * Time.deltaTime);
                transform6.anchoredPosition = Vector2.Lerp(transform6.anchoredPosition, new Vector2(1100f, 0), 5 * Time.deltaTime);
                transform7.anchoredPosition = Vector2.Lerp(transform7.anchoredPosition, new Vector2(2100f, 0), 5 * Time.deltaTime);
                break;
            case (6):
                transform0.anchoredPosition = Vector2.Lerp(transform0.anchoredPosition, new Vector2(-6100f, 0), 5 * Time.deltaTime);
                transform1.anchoredPosition = Vector2.Lerp(transform1.anchoredPosition, new Vector2(-5100f, 0), 5 * Time.deltaTime);
                transform2.anchoredPosition = Vector2.Lerp(transform2.anchoredPosition, new Vector2(-4100f, 0), 5 * Time.deltaTime);
                transform3.anchoredPosition = Vector2.Lerp(transform3.anchoredPosition, new Vector2(-3100f, 0), 5 * Time.deltaTime);
                transform4.anchoredPosition = Vector2.Lerp(transform4.anchoredPosition, new Vector2(-2100f, 0), 5 * Time.deltaTime);
                transform5.anchoredPosition = Vector2.Lerp(transform5.anchoredPosition, new Vector2(-1100f, 0), 5 * Time.deltaTime);
                transform6.anchoredPosition = Vector2.Lerp(transform6.anchoredPosition, new Vector2(0f, 0), 5 * Time.deltaTime);
                transform7.anchoredPosition = Vector2.Lerp(transform7.anchoredPosition, new Vector2(1100f, 0), 5 * Time.deltaTime);
                break;
            case (7):
                transform0.anchoredPosition = Vector2.Lerp(transform0.anchoredPosition, new Vector2(-7100f, 0), 5 * Time.deltaTime);
                transform1.anchoredPosition = Vector2.Lerp(transform1.anchoredPosition, new Vector2(-6100f, 0), 5 * Time.deltaTime);
                transform2.anchoredPosition = Vector2.Lerp(transform2.anchoredPosition, new Vector2(-5100f, 0), 5 * Time.deltaTime);
                transform3.anchoredPosition = Vector2.Lerp(transform3.anchoredPosition, new Vector2(-4100f, 0), 5 * Time.deltaTime);
                transform4.anchoredPosition = Vector2.Lerp(transform4.anchoredPosition, new Vector2(-3100f, 0), 5 * Time.deltaTime);
                transform5.anchoredPosition = Vector2.Lerp(transform5.anchoredPosition, new Vector2(-2100f, 0), 5 * Time.deltaTime);
                transform6.anchoredPosition = Vector2.Lerp(transform6.anchoredPosition, new Vector2(-1100f, 0), 5 * Time.deltaTime);
                transform7.anchoredPosition = Vector2.Lerp(transform7.anchoredPosition, new Vector2(0f, 0), 5 * Time.deltaTime);
                break;
        }

    }
}
