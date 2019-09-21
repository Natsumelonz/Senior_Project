using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using Proyecto26;

public class Chapter_2 : MonoBehaviour
{
    private GameObject Manager;
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
    public Sprite sprite_path;
    public Text event_text;
    public Text speaker;
    public Text textDisplays;
    public DialogManager dialog;

    public GameObject ChapterCanvas;
    public CharObjectOfChapter prefab;

    //ไว้เก็บbutton ที่clone มา
    public Transform container;
    public float space;
    public float lerpSpeed = 5;
    public GameObject pauseTab;
    public List<CharObjectOfChapter> charObjectOfChapter = new List<CharObjectOfChapter>();

    CharObjectOfChapter firstSelected;

    public static Chapter_2 main;

    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        dialog = Manager.GetComponent<DialogManager>();
        //เรียกใช้แสดงประโยค
        foreach (RetrieveDialog item in dialog.dialog_ch2)
        {
            sentences.Add(item.script_desc);
        }
        speaker.text = dialog.dialog_ch2[index].script_role;
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        RepositionObject();

        if (dialog.dialog_ch2[index].script_role == "Teacher")
        {
            teacherPic.SetActive(true);
        }
        else
        {
            teacherPic.SetActive(false);
        }
    }

    public IEnumerator NextSentence()
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
            speaker.text = dialog.dialog_ch2[index].script_role;
            //if (dialog.dialog_ch2[index].script_event == "1")
            //{
            //    _event.text = dialog.dialog_ch2[index].script_desc;
            //}
        }
        else
        {
            event_text.text = "End Chapter 2";
            textDisplays.text = "";
            continueButton.SetActive(false);
            dialogBox.SetActive(false);
            nameDisplay.SetActive(false);
            teacherPic.SetActive(false);

            if (Manager.GetComponent<UserManager>().user.LastCh < 2)
            {
                Manager.GetComponent<UserManager>().user.LastCh = 2;
                Manager.GetComponent<UserManager>().Save(); ;
            }

            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Chapter");
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

        EventCheck();
    }

    void EventCheck()
    {
        if (dialog.dialog_ch2[index].script_event == true)
        {
            switch (dialog.dialog_ch2[index].script_id)
            {
                default:
                    break;
                case ("ch2_004"):
                    event_text.text = "がぎぐげご\nGaGiGuGeGo\nガギグゲゴ";
                    break;
                case ("ch2_005"):
                    event_text.text = "もっと\nMotto";
                    break;
                case ("ch2_006"):
                    event_text.text = "ああ いい うう\nā ī ū\nアー イー ウー";
                    break;
                case ("ch2_007"):
                    event_text.text = "きゃ きゅ きょ\nKya Kyu Kyo\nキャ キュ キョ";
                    break;
                case ("ch2_008"):
                    event_text.text = "が\nand\nかっと";
                    break;
                case ("ch2_010"):
                    sprite_path = Resources.Load<Sprite>("Ch02/tenten");
                    EventImageCall();
                    break;
                case ("ch2_014"):
                    event_text.text = "「か」　ka\n→\n「が」　ga";
                    break;
                case ("ch2_015"):
                    event_text.text = "「き」　ki\n→\n「ぎ」　gi";
                    break;
                case ("ch2_016"):
                    event_text.text = "「く」　ku\n→\n「ぐ」　gu";
                    break;
                case ("ch2_017"):
                    event_text.text = "「け」　ke\n→\n「げ」　ge";
                    break;
                case ("ch2_018"):
                    event_text.text = "「こ」　ko\n→\n「ご」　go";
                    break;
                case ("ch2_020"):
                    event_text.text = "「さ」　sa\n→\n「ざ」　za";
                    break;
                case ("ch2_021"):
                    event_text.text = "「し」　shi\n→\n「じ」　ji";
                    break;
                case ("ch2_022"):
                    event_text.text = "「す」　su\n→\n「ず」　zu";
                    break;
                case ("ch2_023"):
                    event_text.text = "「せ」　se\n→\n「ぜ」　ze";
                    break;
                case ("ch2_024"):
                    event_text.text = "「そ」　so\n→\n「ぞ」　zo";
                    break;
                case ("ch2_026"):
                    event_text.text = "「た」　ta\n→\n「だ」　da";
                    break;
                case ("ch2_027"):
                    event_text.text = "「ち」　chi\n→\n「ぢ」　ji";
                    break;
                case ("ch2_028"):
                    event_text.text = "「つ」　tsu\n→\n「づ」　zu";
                    break;
                case ("ch2_029"):
                    event_text.text = "「て」　te\n→\n「で」　de";
                    break;
                case ("ch2_030"):
                    event_text.text = "「と」　to\n→\n「ど」　do";
                    break;
                case ("ch2_032"):
                    event_text.text = "「は」　ha\n→\n「ば」　ba";
                    break;
                case ("ch2_033"):
                    event_text.text = "「ひ」　hi\n→\n「び」　bi";
                    break;
                case ("ch2_034"):
                    event_text.text = "「ふ」　fu\n→\n「ぶ」　bu";
                    break;
                case ("ch2_035"):
                    event_text.text = "「へ」　he\n→\n「べ」　be";
                    break;
                case ("ch2_036"):
                    event_text.text = "「ほ」　ho\n→\n「ぼ」　bo";
                    break;
                case ("ch2_038"):
                    sprite_path = Resources.Load<Sprite>("Ch02/maru");
                    EventImageCall();
                    break;
                case ("ch2_041"):
                    event_text.text = "「は」　ha\n→\n「ぱ」　pa";
                    break;
                case ("ch2_042"):
                    event_text.text = "「ひ」　hi\n→\n「ぴ」　pi";
                    break;
                case ("ch2_043"):
                    event_text.text = "「ふ」　fu\n→\n「ぷ」　pu";
                    break;
                case ("ch2_044"):
                    event_text.text = "「へ」　he\n→\n「ぺ」　pe";
                    break;
                case ("ch2_045"):
                    event_text.text = "「ほ」　ho\n→\n「ぽ」　po";
                    break;
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
        foreach (string s in dialog.question_ch2[qindex].question_choice)
        {
            CharObjectOfChapter clone = Instantiate(prefab.gameObject).GetComponent<CharObjectOfChapter>();
            clone.transform.SetParent(container);
            clone.transform.localScale = new Vector3(1, 1, 1);
            charObjectOfChapter.Add(clone.Init(s));
        }
    }

    public void CheckAnswer(CharObjectOfChapter charObjectOfChapters)
    {
        if (charObjectOfChapters.sentence == dialog.question_ch2[qindex].correct_answer)
        {
            charObjectOfChapter.Clear();
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }
            event_image.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

            textDisplays.text = "Very good!";
            event_text.text = "";
            speaker.text = dialog.dialog_ch2[index].script_role;
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

            textDisplays.text = "Wrong! try again.";
            event_text.text = "";
            speaker.text = dialog.dialog_ch2[index].script_role;
            index--;
            continueButton.SetActive(true);
        }
    }

    public void PauseButton()
    {
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
        StartCoroutine(NextSentence());
    }

    public void NextSentenceB()
    {
        StartCoroutine(NextSentence());
    }
}
