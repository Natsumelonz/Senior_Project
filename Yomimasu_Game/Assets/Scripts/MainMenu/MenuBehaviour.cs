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
    }

    public void Mute()
    {
        if (Audio.GetComponent<AudioSource>().mute)
        {
            Audio.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            Audio.GetComponent<AudioSource>().mute = true;
        }        
    }    

    public void TriggerMenuBehaviour(int i)
    {
        switch (i)
        {
            default:
                Effect.GetComponent<AudioSource>().Play();
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
                Audio.GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("GameMatching");
                break;
            case (3):
                Effect.GetComponent<AudioSource>().Play();
                Audio.GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("SortingWord");
                break;            
            case (4):
                Effect.GetComponent<AudioSource>().Play();
                Audio.GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("Chapter");
                break;          
            case (5):
                Effect.GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("GameMenu");
                break;
        }
    }
}
