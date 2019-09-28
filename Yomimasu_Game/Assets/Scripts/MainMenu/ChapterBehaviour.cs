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

    public GameObject Panel;
    public AudioClip effectClip;
    public AudioSource effectAudio;

    private void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        Audio = GameObject.Find("AudioManager").gameObject;
        Effect = GameObject.Find("EffectManager").gameObject;

        Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMChapterSelect;
        Audio.GetComponent<AudioSource>().Play();
    }

    public void ClosePanel()
    {
        Effect.GetComponent<AudioSource>().Play();
        Panel.SetActive(false);
    }

    public void TriggerChapterBehaviour(int i)
    {
        switch (i)
        {
            default:
                Effect.GetComponent<AudioSource>().Play();
                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMMainMenu;
                Audio.GetComponent<AudioSource>().Play();

                SceneManager.LoadScene("MainMenu");
                break;
            case (0):
                Effect.GetComponent<AudioSource>().Play();

                if (Manager.GetComponent<UserManager>().user.LastCh < 4)
                {
                    Panel.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("AnotherChapter");
                }
                break;
            case (1):
                Effect.GetComponent<AudioSource>().Play();

                if (!Manager.GetComponent<UserManager>().user.PassPre[0])
                {
                    Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMCh1;
                    Audio.GetComponent<AudioSource>().Play();
                    SceneManager.LoadScene("TestCh1");
                }
                else
                {
                    Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMCh1;
                    Audio.GetComponent<AudioSource>().Play();

                    ChapterScene.dialogThis = Manager.GetComponent<ChapterManager>().dialog_ch1;
                    ChapterScene.questionThis = Manager.GetComponent<ChapterManager>().question_ch1;
                    SceneManager.LoadScene("ChapterScene");
                }
                break;
            case (2):
                Effect.GetComponent<AudioSource>().Play();

                if (Manager.GetComponent<UserManager>().user.LastCh < 1)
                {
                    Panel.SetActive(true);
                }
                else
                {
                    Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMCh2;
                    Audio.GetComponent<AudioSource>().Play();

                    ChapterScene.dialogThis = Manager.GetComponent<ChapterManager>().dialog_ch2;
                    ChapterScene.questionThis = Manager.GetComponent<ChapterManager>().question_ch2;
                    SceneManager.LoadScene("ChapterScene");
                }
                break;
            case (3):
                Effect.GetComponent<AudioSource>().Play();

                if (Manager.GetComponent<UserManager>().user.LastCh < 2)
                {
                    Panel.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("Chapter_3");
                }
                break;
            case (4):
                Effect.GetComponent<AudioSource>().Play();

                if (Manager.GetComponent<UserManager>().user.LastCh < 3)
                {
                    Panel.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("Chapter_4");
                }
                break;
            case (5):
                Effect.GetComponent<AudioSource>().Play();

                if (Manager.GetComponent<UserManager>().user.LastCh < 4)
                {
                    Panel.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("Chapter_5");
                }
                break;
        }
    }
}
