using System.Collections;
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

    public GameObject PreviousPanel;
    public GameObject ObjectivePanel;
    public Button alphabetChartButton;
    public AudioClip effectClip;
    public AudioSource effectAudio;
    public Text chapter;
    public Text chapterName;
    public Text objective;
    public List<GameObject> ChapterButton;
    public List<GameObject> NextBack;

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

        SetProcess();
    }

    private void Update()
    {
    }

    public void ClosePanel()
    {
        Effect.GetComponent<AudioSource>().Play();
        PreviousPanel.SetActive(false);
        ObjectivePanel.SetActive(false);
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
            case (-1):
                NextBack[1].SetActive(false);
                NextBack[0].SetActive(true);
                for (int x = 0; x < 10; x++)
                {
                    if (x > 4)
                    {

                        ChapterButton[x].SetActive(false);
                    }
                    else
                    {
                        ChapterButton[x].SetActive(true);
                    }
                }
                break;
            case (0):
                if (Manager.GetComponent<UserManager>().user.LastCh < 5)
                {
                    PreviousPanel.SetActive(true);
                }
                else
                {
                    NextBack[0].SetActive(false);
                    NextBack[1].SetActive(true);
                    for (int x = 0; x < 10; x++)
                    {
                        if (x < 4)
                        {

                            ChapterButton[x].SetActive(false);
                        }
                        else
                        {
                            ChapterButton[x].SetActive(true);
                        }
                    }
                }
                break;
            case (1):
                ObjectivePanel.SetActive(true);
                chapter.text = "Chapter 1";
                chapterName.text = Manager.GetComponent<ContentManager>().chapter1_info.Name;
                objective.text = Manager.GetComponent<ContentManager>().chapter1_info.Objective;

                ChapterScene.dialogThis = Manager.GetComponent<ContentManager>().chapter1_info.Scripts;
                ChapterScene.questionThis = Manager.GetComponent<ContentManager>().chapter1_info.Questions;
                ChapterScene.chName = Manager.GetComponent<ContentManager>().chapter1_info.Name;
                TestScene.testhis = Manager.GetComponent<ContentManager>().chapter1_info.Tests;

                break;
            case (2):
                if (Manager.GetComponent<UserManager>().user.LastCh < 1)
                {
                    PreviousPanel.SetActive(true);
                }
                else
                {
                    ObjectivePanel.SetActive(true);
                    chapter.text = "Chapter 2";
                    chapterName.text = Manager.GetComponent<ContentManager>().chapter2_info.Name;
                    objective.text = Manager.GetComponent<ContentManager>().chapter2_info.Objective;

                    ChapterScene.dialogThis = Manager.GetComponent<ContentManager>().chapter2_info.Scripts;
                    ChapterScene.questionThis = Manager.GetComponent<ContentManager>().chapter2_info.Questions;
                    ChapterScene.chName = Manager.GetComponent<ContentManager>().chapter2_info.Name;
                    TestScene.testhis = Manager.GetComponent<ContentManager>().chapter2_info.Tests;
                }
                break;
            case (3):
                if (Manager.GetComponent<UserManager>().user.LastCh < 2)
                {
                    PreviousPanel.SetActive(true);
                }
                else
                {
                    ObjectivePanel.SetActive(true);
                    chapter.text = "Chapter 3";
                    chapterName.text = Manager.GetComponent<ContentManager>().chapter3_info.Name;
                    objective.text = Manager.GetComponent<ContentManager>().chapter3_info.Objective;

                    ChapterScene.dialogThis = Manager.GetComponent<ContentManager>().chapter3_info.Scripts;
                    ChapterScene.questionThis = Manager.GetComponent<ContentManager>().chapter3_info.Questions;
                    ChapterScene.chName = Manager.GetComponent<ContentManager>().chapter3_info.Name;
                    TestScene.testhis = Manager.GetComponent<ContentManager>().chapter3_info.Tests;
                }
                break;
            case (4):
                if (Manager.GetComponent<UserManager>().user.LastCh < 3)
                {
                    PreviousPanel.SetActive(true);
                }
                else
                {
                    ObjectivePanel.SetActive(true);
                    chapter.text = "Chapter 4";
                    chapterName.text = Manager.GetComponent<ContentManager>().chapter4_info.Name;
                    objective.text = Manager.GetComponent<ContentManager>().chapter4_info.Objective;

                    ChapterScene.dialogThis = Manager.GetComponent<ContentManager>().chapter4_info.Scripts;
                    ChapterScene.questionThis = Manager.GetComponent<ContentManager>().chapter4_info.Questions;
                    ChapterScene.chName = Manager.GetComponent<ContentManager>().chapter4_info.Name;
                    TestScene.testhis = Manager.GetComponent<ContentManager>().chapter4_info.Tests;
                }
                break;
            case (5):
                if (Manager.GetComponent<UserManager>().user.LastCh < 4)
                {
                    PreviousPanel.SetActive(true);
                }
                else
                {
                    ObjectivePanel.SetActive(true);
                    chapter.text = "Chapter 5";
                    chapterName.text = Manager.GetComponent<ContentManager>().chapter5_info.Name;
                    objective.text = Manager.GetComponent<ContentManager>().chapter5_info.Objective;

                    ChapterScene.dialogThis = Manager.GetComponent<ContentManager>().chapter5_info.Scripts;
                    ChapterScene.questionThis = Manager.GetComponent<ContentManager>().chapter5_info.Questions;
                    ChapterScene.chName = Manager.GetComponent<ContentManager>().chapter5_info.Name;
                    TestScene.testhis = Manager.GetComponent<ContentManager>().chapter5_info.Tests;
                }
                break;
            case (6):
                if (Manager.GetComponent<UserManager>().user.LastCh < 5)
                {
                    PreviousPanel.SetActive(true);
                }
                else
                {
                    ObjectivePanel.SetActive(true);
                    chapter.text = "Chapter 6";
                    chapterName.text = Manager.GetComponent<ContentManager>().chapter6_info.Name;
                    objective.text = Manager.GetComponent<ContentManager>().chapter6_info.Objective;

                    ChapterScene.dialogThis = Manager.GetComponent<ContentManager>().chapter6_info.Scripts;
                    ChapterScene.questionThis = Manager.GetComponent<ContentManager>().chapter6_info.Questions;
                    ChapterScene.chName = Manager.GetComponent<ContentManager>().chapter6_info.Name;
                    TestScene.testhis = Manager.GetComponent<ContentManager>().chapter6_info.Tests;
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
                if (!Manager.GetComponent<UserManager>().user.PassPre[5])
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
        ChapterButton[0].GetComponentInChildren<Slider>().value = ((float)Manager.GetComponent<UserManager>().user.LastIndex[0]
            / (float)Manager.GetComponent<ContentManager>().chapter1_info.Scripts.Count);
        ChapterButton[0].GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text =
            string.Format("{0:0.00}", (((float)Manager.GetComponent<UserManager>().user.LastIndex[0]
            / (float)Manager.GetComponent<ContentManager>().chapter1_info.Scripts.Count) * 100)) + "%";
        
        ChapterButton[1].GetComponentInChildren<Slider>().value = ((float)Manager.GetComponent<UserManager>().user.LastIndex[1]
            / (float)Manager.GetComponent<ContentManager>().chapter2_info.Scripts.Count);
        ChapterButton[1].GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text =
            string.Format("{0:0.00}", (((float)Manager.GetComponent<UserManager>().user.LastIndex[1]
            / (float)Manager.GetComponent<ContentManager>().chapter2_info.Scripts.Count) * 100)) + "%";
        
        ChapterButton[2].GetComponentInChildren<Slider>().value = ((float)Manager.GetComponent<UserManager>().user.LastIndex[2]
            / (float)Manager.GetComponent<ContentManager>().chapter3_info.Scripts.Count);
        ChapterButton[2].GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text =
            string.Format("{0:0.00}", (((float)Manager.GetComponent<UserManager>().user.LastIndex[2]
            / (float)Manager.GetComponent<ContentManager>().chapter3_info.Scripts.Count) * 100)) + "%";
        
        ChapterButton[3].GetComponentInChildren<Slider>().value = ((float)Manager.GetComponent<UserManager>().user.LastIndex[3]
            / (float)Manager.GetComponent<ContentManager>().chapter4_info.Scripts.Count);
        ChapterButton[3].GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text =
            string.Format("{0:0.00}", (((float)Manager.GetComponent<UserManager>().user.LastIndex[3]
            / (float)Manager.GetComponent<ContentManager>().chapter4_info.Scripts.Count) * 100)) + "%";
        
        ChapterButton[4].GetComponentInChildren<Slider>().value = ((float)Manager.GetComponent<UserManager>().user.LastIndex[4]
            / (float)Manager.GetComponent<ContentManager>().chapter5_info.Scripts.Count);
        ChapterButton[4].GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text =
            string.Format("{0:0.00}", (((float)Manager.GetComponent<UserManager>().user.LastIndex[4]
            / (float)Manager.GetComponent<ContentManager>().chapter5_info.Scripts.Count) * 100)) + "%";
        
        ChapterButton[5].GetComponentInChildren<Slider>().value = ((float)Manager.GetComponent<UserManager>().user.LastIndex[5]
            / (float)Manager.GetComponent<ContentManager>().chapter6_info.Scripts.Count);
        ChapterButton[5].GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text =
            string.Format("{0:0.00}", (((float)Manager.GetComponent<UserManager>().user.LastIndex[5]
            / (float)Manager.GetComponent<ContentManager>().chapter6_info.Scripts.Count) * 100)) + "%";

        ChapterButton[6].GetComponentInChildren<Slider>().value = ((float)Manager.GetComponent<UserManager>().user.LastIndex[6]
            / (float)Manager.GetComponent<ContentManager>().chapter7_info.Scripts.Count);
        ChapterButton[6].GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text =
            string.Format("{0:0.00}", (((float)Manager.GetComponent<UserManager>().user.LastIndex[0]
            / (float)Manager.GetComponent<ContentManager>().chapter7_info.Scripts.Count) * 100)) + "%";

        ChapterButton[7].GetComponentInChildren<Slider>().value = ((float)Manager.GetComponent<UserManager>().user.LastIndex[7]
            / (float)Manager.GetComponent<ContentManager>().chapter8_info.Scripts.Count);
        ChapterButton[7].GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text =
            string.Format("{0:0.00}", (((float)Manager.GetComponent<UserManager>().user.LastIndex[0]
            / (float)Manager.GetComponent<ContentManager>().chapter8_info.Scripts.Count) * 100)) + "%";

        ChapterButton[8].GetComponentInChildren<Slider>().value = ((float)Manager.GetComponent<UserManager>().user.LastIndex[8]
            / (float)Manager.GetComponent<ContentManager>().chapter9_info.Scripts.Count);
        ChapterButton[8].GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text =
            string.Format("{0:0.00}", (((float)Manager.GetComponent<UserManager>().user.LastIndex[0]
            / (float)Manager.GetComponent<ContentManager>().chapter9_info.Scripts.Count) * 100)) + "%";

        ChapterButton[9].GetComponentInChildren<Slider>().value = ((float)Manager.GetComponent<UserManager>().user.LastIndex[9]
            / (float)Manager.GetComponent<ContentManager>().chapter10_info.Scripts.Count);
        ChapterButton[9].GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text =
            string.Format("{0:0.00}", (((float)Manager.GetComponent<UserManager>().user.LastIndex[0]
            / (float)Manager.GetComponent<ContentManager>().chapter10_info.Scripts.Count) * 100)) + "%";
    }
}
