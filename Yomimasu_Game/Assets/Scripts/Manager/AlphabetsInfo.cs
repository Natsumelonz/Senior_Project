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
    private int alphabetplus = 0;
    private bool hira = true;

    public List<AudioClip> listenClip;
    public List<RuntimeAnimatorController> controllers;
    public AudioSource listenSource;
    public Text alphabetText;
    public GameObject writingImage;
    public List<GameObject> alphabetChart;
    public List<Button> switchButton;
    public List<Sprite> imageSpirte;

    private void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        Audio = GameObject.Find("AudioManager").gameObject;
        Effect = GameObject.Find("EffectManager").gameObject;

        if (Manager.GetComponent<UserManager>().user.katakana)
        {
            for (int i = 0; i < switchButton.Count; i++)
            {
                switchButton[i].interactable = true;
            }
        }
    }

    public void ListenSound()
    {
        listenSource.PlayOneShot(listenClip[alphabet]);
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

    public void SwitchAlphabet(int i)
    {
        Effect.GetComponent<AudioSource>().Play();

        for (int x = 0; x < alphabetChart.Count; x++)
        {
            if (x == i)
            {
                alphabetChart[x].SetActive(true);
            }
            else
            {
                alphabetChart[x].SetActive(false);
            }
        }

        switch (i)
        {
            default:
                break;
            case (0):                
                hira = true;

                alphabet = 0;
                alphabetText.text = Manager.GetComponent<AlphabetManager>().alphabetsHR[0].alphabetname_romanji;
                writingImage.GetComponent<Animator>().runtimeAnimatorController = controllers[0];
                break;
            case (1):
                hira = false;

                alphabet = 0;
                alphabetText.text = Manager.GetComponent<AlphabetManager>().alphabetsKT[0].alphabetname_romanji;
                writingImage.GetComponent<Animator>().runtimeAnimatorController = controllers[(0 + 46)];
                break;
            case (2):
                hira = true;

                alphabet = 46;
                alphabetText.text = Manager.GetComponent<AlphabetManager>().alphabetsHR[46].alphabetname_romanji;
                writingImage.GetComponent<Animator>().runtimeAnimatorController = null;
                writingImage.GetComponent<Image>().sprite = imageSpirte[0];
                break;
            case (3):
                hira = false;

                alphabet = 46;
                alphabetText.text = Manager.GetComponent<AlphabetManager>().alphabetsKT[46].alphabetname_romanji;
                writingImage.GetComponent<Animator>().runtimeAnimatorController = null;
                writingImage.GetComponent<Image>().sprite = imageSpirte[25];
                break;
            case (4):
                hira = true;

                alphabet = 71;
                alphabetText.text = Manager.GetComponent<AlphabetManager>().alphabetsHR[71].alphabetname_romanji;
                writingImage.GetComponent<Animator>().runtimeAnimatorController = null;
                writingImage.GetComponent<Image>().sprite = imageSpirte[50];
                break;
            case (5):
                hira = false;

                alphabet = 71;
                alphabetText.text = Manager.GetComponent<AlphabetManager>().alphabetsKT[71].alphabetname_romanji;
                writingImage.GetComponent<Animator>().runtimeAnimatorController = null;
                writingImage.GetComponent<Image>().sprite = imageSpirte[83];
                break;
        }
    }

    public void MainMenu()
    {
        Effect.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Chapter");
    }

    public void SelectAlphabet(int i)
    {
        Effect.GetComponent<AudioSource>().Play();
        if (hira)
        {
            alphabet = i;
            writingImage.GetComponent<Animator>().runtimeAnimatorController = controllers[i];
            alphabetText.text = Manager.GetComponent<AlphabetManager>().alphabetsHR[i].alphabetname_romanji;
        }
        else
        {
            alphabet = (i);
            writingImage.GetComponent<Animator>().runtimeAnimatorController = controllers[(i + 46)];
            alphabetText.text = Manager.GetComponent<AlphabetManager>().alphabetsKT[i].alphabetname_romanji;
        }
    }

    public void SelectAlphabetPlus(int i)
    {
        Effect.GetComponent<AudioSource>().Play();
        if (hira)
        {
            alphabet = i;
            alphabetplus = i - 46;
            alphabetText.text = Manager.GetComponent<AlphabetManager>().alphabetsHR[i].alphabetname_romanji;
            writingImage.GetComponent<Image>().sprite = imageSpirte[alphabetplus];
            writingImage.GetComponent<Animator>().runtimeAnimatorController = null;
        }
        else
        {
            alphabet = i;
            alphabetplus = i - 46;
            alphabetText.text = Manager.GetComponent<AlphabetManager>().alphabetsKT[i].alphabetname_romanji;
            writingImage.GetComponent<Image>().sprite = imageSpirte[(alphabetplus + 25)];
            writingImage.GetComponent<Animator>().runtimeAnimatorController = null;
        }
    }

    public void SelectAlphabetPlusPlus(int i)
    {
        Effect.GetComponent<AudioSource>().Play();
        if (hira)
        {
            alphabet = i;
            alphabetplus = i - 46;
            alphabetText.text = Manager.GetComponent<AlphabetManager>().alphabetsHR[i].alphabetname_romanji;
            writingImage.GetComponent<Image>().sprite = imageSpirte[alphabetplus + 25];
            writingImage.GetComponent<Animator>().runtimeAnimatorController = null;
        }
        else
        {
            alphabet = i;
            alphabetplus = i - 46;
            alphabetText.text = Manager.GetComponent<AlphabetManager>().alphabetsKT[i].alphabetname_romanji;
            writingImage.GetComponent<Image>().sprite = imageSpirte[(alphabetplus + 58)];
            writingImage.GetComponent<Animator>().runtimeAnimatorController = null;
        }
    }
}
