using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Proyecto26;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
    private GameObject Manager;
    private GameObject Audio;
    private GameObject Effect;

    public Text playerName;
    public GameObject matchingPanel;
    public Button quitPanel;
    public Button kataLevel;

    void OnDisable()
    {
        PlayerPrefs.SetInt("level", 1);
    }

    private void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        Audio = GameObject.Find("AudioManager").gameObject;
        Effect = GameObject.Find("EffectManager").gameObject;
        playerName.text = Manager.GetComponent<UserManager>().user.Name;

        Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMMainMenu;        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Mute()
    {
        Effect.GetComponent<AudioSource>().Play();
        if (Audio.GetComponent<AudioSource>().mute)
        {
            Audio.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            Audio.GetComponent<AudioSource>().mute = true;
        }
    }

    public void MatchingLevel(int i)
    {
        Effect.GetComponent<AudioSource>().Play();

        if (Manager.GetComponent<UserManager>().user.katakana)
        {
            kataLevel.interactable = true;
        }

        switch (i)
        {
            default:
                matchingPanel.SetActive(false);
                break;
            case (0):
                MatchingManager.hiragana = true;
                Audio.GetComponent<AudioSource>().Stop();

                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMMatching;
                Audio.GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("GameMatching");
                break;
            case (1):
                MatchingManager.hiragana = false;
                Audio.GetComponent<AudioSource>().Stop();

                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMMatching;
                Audio.GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("GameMatching");
                break;
        }
    }

    public void TriggerMenuBehaviour(int i)
    {
        switch (i)
        {
            default:
                Effect.GetComponent<AudioSource>().Play();
                Manager.GetComponent<UserManager>().SaveUser();

                Application.Quit();
                break;
            case (0):
                Effect.GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("MainMenu");
                break;
            case (1):
                Effect.GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("PlayerInfo");
                break;
            case (2):
                Effect.GetComponent<AudioSource>().Play();
                matchingPanel.SetActive(true);                
                break;
            case (3):
                Effect.GetComponent<AudioSource>().Play();
                Audio.GetComponent<AudioSource>().Stop();

                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMScramble;
                Audio.GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("SortingWord");
                break;
            case (4):
                Effect.GetComponent<AudioSource>().Play();
                Audio.GetComponent<AudioSource>().Stop();

                Audio.GetComponent<AudioSource>().clip = Audio.GetComponent<AudioManager>().BGMChapterSelect;
                Audio.GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("Chapter");
                break;
            case (5):
                Effect.GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("GameMenu");
                break;
        }
    }
}
