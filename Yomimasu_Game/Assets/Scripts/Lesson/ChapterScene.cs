using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using Proyecto26;

public class ChapterScene : MonoBehaviour
{
    private GameObject Manager;
    private GameObject Audio;
    private GameObject Effect;
    public List<string> sentences = new List<string>();
    private int index;
    private int qindex;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject dialogBox;
    public GameObject teacherPic;
    public GameObject nameDisplay;
    public GameObject event_image;
    public Text skip;
    public Toggle skipBool;
    public Sprite sprite_path;
    public Text event_text;
    public Text speaker;
    public Text textDisplays;

    public GameObject ChapterCanvas;
    public CharObjectOfChapter prefab;

    //ไว้เก็บbutton ที่clone มา
    public Transform container;
    public float space;
    public float lerpSpeed = 5;
    public GameObject pauseTab;
    public List<CharObjectOfChapter> charObjectOfChapter = new List<CharObjectOfChapter>();

    //CharObjectOfChapter firstSelected;

    public static ChapterScene main;
    public static List<RetrieveDialog> dialogThis;
    public static List<RetrieveQuestion> questionThis;

    public AudioClip correctClip;
    public AudioClip wrongClip;
    public AudioSource effectSource;

    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        Audio = GameObject.Find("AudioManager").gameObject;
        Effect = GameObject.Find("EffectManager").gameObject;

        //เรียกใช้แสดงประโยค
        foreach (RetrieveDialog item in dialogThis)
        {
            sentences.Add(item.script_desc);
        }
        speaker.text = dialogThis[index].script_role;
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        RepositionObject();

        if (dialogThis[index].script_role == "Teacher")
        {
            teacherPic.SetActive(true);
        }
        else
        {
            teacherPic.SetActive(false);
        }
    }

    public void NextSentence()
    {
        //ปิดปุ่มContinue
        continueButton.SetActive(false);
        charObjectOfChapter.Clear();
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        //ถ้าindexประโยคยังไม่หมดทำต่อ       

        event_image.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

        if (index < sentences.Count - 1)
        {
            index++;
            textDisplays.text = "";
            event_text.text = "";
            StartCoroutine(Type());
            speaker.text = dialogThis[index].script_role;
        }
        else
        {
            event_text.text = "End Chapter 1";
            textDisplays.text = "";
            continueButton.SetActive(false);
            dialogBox.SetActive(false);
            nameDisplay.SetActive(false);
            teacherPic.SetActive(false);

            if (Manager.GetComponent<UserManager>().user.LastCh < 1)
            {
                Manager.GetComponent<UserManager>().user.LastCh = 1;
                Manager.GetComponent<UserManager>().Save();
            }

            if (!Manager.GetComponent<UserManager>().user.PassPost[0])
            {
                SceneManager.LoadScene("TestCh1");
            }
            else
            {
                SceneManager.LoadScene("Chapter");
            }
        }
    }

    //สร้างมาเพื่อให้ textDisplays ค่อยๆแสดงประโยคที่มีในarrayออกมา โดยใช้ typingSpeedเป็นตัวหน่วงเวลา
    IEnumerator Type()
    {
        dialogBox.SetActive(true);
        nameDisplay.SetActive(true);
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplays.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        //ถ้าคำที่แสดง แสดงจนครบประโยคแล้วถึงกดไปต่อได้
        if (textDisplays.text == sentences[index])
        {
            continueButton.SetActive(true);
        }

        if (skipBool.isOn)
        {
            NextSentence();
        }

        EventCheck();
    }

    void EventCheck()
    {
        if (dialogThis[index].script_event == true)
        {
            event_text.text = dialogThis[index].script_event_text;

            if (dialogThis[index].script_sprite_path != null)
            {
                sprite_path = Resources.Load<Sprite>(dialogThis[index].script_sprite_path);
                EventImageCall();
            }

            if (dialogThis[index].script_question)
            {
                continueButton.SetActive(false);
                CloneChoice();
            }
        }
    }

    void EventImageCall()
    {
        event_image.GetComponent<Image>().sprite = sprite_path;
        event_image.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        event_image.GetComponent<Image>().preserveAspect = true;
    }

    void RepositionObject()
    {

        if (charObjectOfChapter.Count == 0)
        {
            return;
        }
        float center = (charObjectOfChapter.Count + 1f) / 2;
        for (int i = 0; i < charObjectOfChapter.Count; i++)
        {
            charObjectOfChapter[i].rectTransform.anchoredPosition
                = Vector2.Lerp(charObjectOfChapter[i].rectTransform.anchoredPosition,
                 new Vector2(0, (center - i) * space), lerpSpeed * Time.deltaTime);
            charObjectOfChapter[i].index = i;
        }
    }
    void Awake()
    {
        main = this;
    }

    void CloneChoice()
    {
        qindex = questionThis.FindIndex(item => item.script_id == dialogThis[index].script_id);

        foreach (string s in questionThis[qindex].question_choice)
        {
            CharObjectOfChapter clone = Instantiate(prefab.gameObject).GetComponent<CharObjectOfChapter>();
            clone.transform.SetParent(container);
            clone.transform.localScale = new Vector3(1, 1, 1);
            charObjectOfChapter.Add(clone.Init(s));
        }
    }

    public void CheckAnswer(CharObjectOfChapter charObjectOfChapters)
    {
        if (charObjectOfChapters.sentence == questionThis[qindex].correct_answer)
        {
            charObjectOfChapter.Clear();
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }
            event_image.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

            effectSource.PlayOneShot(correctClip);
            textDisplays.text = "Very good!";
            event_text.text = "";
            speaker.text = dialogThis[index].script_role;
            continueButton.SetActive(true);
        }
        else
        {
            charObjectOfChapter.Clear();
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }
            event_image.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

            effectSource.PlayOneShot(wrongClip);
            textDisplays.text = "Wrong! try again.";
            event_text.text = "";
            speaker.text = dialogThis[index].script_role;
            index--;
            continueButton.SetActive(true);
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
        }
    }

    public void Skip()
    {
        index = Int32.Parse(skip.text) - 1;
        NextSentence();
    }
}
