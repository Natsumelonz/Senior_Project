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
    public AudioClip effectClip;
    public AudioSource effectAudio;
    public Text chapter;
    public Text chapterName;
    public Text objective;

    private int chapterid;

    private void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        Audio = GameObject.Find("AudioManager").gameObject;
        Effect = GameObject.Find("EffectManager").gameObject;
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

        switch (i)
        {
            default:
                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMMainMenu;
                Audio.GetComponent<AudioSource>().Play();

                SceneManager.LoadScene("MainMenu");
                break;
            case (-1):
                SceneManager.LoadScene("Alphabets Chart");
                break;
            case (0):
                if (Manager.GetComponent<UserManager>().user.LastCh < 4)
                {
                    PreviousPanel.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("AnotherChapter");
                }
                break;
            case (1):
                ObjectivePanel.SetActive(true);
                chapter.text = "Chapter 1";
                chapterName.text = Manager.GetComponent<ContentManager>().chapter1_info.Name;
                objective.text = Manager.GetComponent<ContentManager>().chapter1_info.Objective;

                chapterid = 1;

                ChapterScene.dialogThis = Manager.GetComponent<ContentManager>().chapter1_info.Scripts;
                ChapterScene.questionThis = Manager.GetComponent<ContentManager>().chapter1_info.Questions;
                ChapterScene.tindex = 0;
                ChapterScene.chName = Manager.GetComponent<ContentManager>().chapter1_info.Name;
                TestScene.testhis = Manager.GetComponent<ContentManager>().chapter1_info.Tests;
                TestScene.sindex = 0;
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

                    chapterid = 2;

                    ChapterScene.dialogThis = Manager.GetComponent<ContentManager>().chapter2_info.Scripts;
                    ChapterScene.questionThis = Manager.GetComponent<ContentManager>().chapter2_info.Questions;
                    ChapterScene.tindex = 1;
                    ChapterScene.chName = Manager.GetComponent<ContentManager>().chapter2_info.Name;
                    TestScene.testhis = Manager.GetComponent<ContentManager>().chapter2_info.Tests;
                    TestScene.sindex = 1;
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
                    //chapterName.text = Manager.GetComponent<ContentManager>().chapter3_info.Name;
                    //objective.text = Manager.GetComponent<ContentManager>().chapter3_info.Objective;

                    chapterid = 3;
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
                    //chapterName.text = Manager.GetComponent<ContentManager>().chapter4_info.Name;
                    //objective.text = Manager.GetComponent<ContentManager>().chapter4_info.Objective;

                    chapterid = 4;
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
                    //chapterName.text = Manager.GetComponent<ContentManager>().chapter5_info.Name;
                    //objective.text = Manager.GetComponent<ContentManager>().chapter5_info.Objective;

                    chapterid = 5;
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
                    SceneManager.LoadScene("Chapter_3");
                }
                break;
            case (4):
                if (!Manager.GetComponent<UserManager>().user.PassPre[3])
                {
                    SceneManager.LoadScene("TestScene");
                }
                else
                {
                    SceneManager.LoadScene("Chapter_4");
                }
                break;
            case (5):
                if (!Manager.GetComponent<UserManager>().user.PassPre[4])
                {
                    SceneManager.LoadScene("TestScene");
                }
                else
                {
                    SceneManager.LoadScene("Chapter_5");
                }
                break;
        }
    }
}
