using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlphabetsInfo : MonoBehaviour
{
    private GameObject Manager;
    private GameObject Effect;
    private GameObject Audio;
    private int alphabet = 0;
    private bool hira = true;

    public List<AudioClip> listenClip;
    public List<RuntimeAnimatorController> controllers;
    public AudioSource listenSource;
    public Text alphabetText;
    public GameObject writingImage;
    public GameObject hiraganaChart;
    public GameObject katakanaChart;

    private void Start()
    {
        //Manager = GameObject.Find("GameData").gameObject;
        //Audio = GameObject.Find("AudioManager").gameObject;
        //Effect = GameObject.Find("EffectManager").gameObject;
    }    

    public void ListenSound()
    {
        listenSource.PlayOneShot(listenClip[alphabet]);
    }

    public void SwitchAlphabet(int i)
    {
        switch (i)
        {
            default:
                break;
            case (0):
                hiraganaChart.SetActive(true);
                katakanaChart.SetActive(false);
                hira = true;

                alphabet = 0;
                writingImage.GetComponent<Animator>().runtimeAnimatorController = controllers[0];
                break;
            case (1):
                hiraganaChart.SetActive(false);
                katakanaChart.SetActive(true);
                hira = false;

                alphabet = (0 + 46);
                writingImage.GetComponent<Animator>().runtimeAnimatorController = controllers[(0 + 46)];
                break;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Chapter");
    }

    public void SelectAlphabet(int i)
    {
        if (hira)
        {
            alphabet = i;
            writingImage.GetComponent<Animator>().runtimeAnimatorController = controllers[i];
            //alphabetText.text = Manager.GetComponent<AlphabetManager>().alphabetsHR[i].alphabetname_romanji;
        }
        else
        {
            alphabet = (i+46);
            writingImage.GetComponent<Animator>().runtimeAnimatorController = controllers[(i+46)];
            //alphabetText.text = Manager.GetComponent<AlphabetManager>().alphabetsKT[i].alphabetname_romanji;
        }
    }
}
