using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using Proyecto26;

public class Chapter_1 : MonoBehaviour
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

    public static Chapter_1 main;

    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        dialog = Manager.GetComponent<DialogManager>();
        //เรียกใช้แสดงประโยค
        foreach (RetrieveDialog item in dialog.dialog_ch1)
        {
            sentences.Add(item.script_desc);
        }
        speaker.text = dialog.dialog_ch1[index].script_role;
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        RepositionObject();

        if (dialog.dialog_ch1[index].script_role == "Teacher")
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
            speaker.text = dialog.dialog_ch1[index].script_role;
            //if (dialog.dialog_ch1[index].script_event == "1")
            //{
            //    _event.text = dialog.dialog_ch1[index].script_desc;
            //}
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
        if (dialog.dialog_ch1[index].script_event == true)
        {
            switch (dialog.dialog_ch1[index].script_id)
            {
                default:
                    break;
                case ("ch1_005"):
                    event_text.text = "ひらがな";
                    break;
                case ("ch1_006"):
                    event_text.text = "カタカナ";
                    break;
                case ("ch1_007"):
                    event_text.text = "漢字";
                    break;
                case ("ch1_011"):
                    sprite_path = Resources.Load<Sprite>("Ch01/hiragana_chart");
                    EventImageCall();
                    break;
                case ("ch1_012"):
                    sprite_path = Resources.Load<Sprite>("Ch01/dictionary");
                    EventImageCall();
                    break;
                case ("ch1_017"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_018"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_019"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_020"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_021"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_023"):
                    continueButton.SetActive(false);
                    qindex = 0;
                    event_text.text = "い";
                    CloneChoice();
                    break;
                case ("ch1_024"):
                    continueButton.SetActive(false);
                    qindex = 1;
                    event_text.text = "e";
                    CloneChoice();
                    break;
                case ("ch1_025"):
                    continueButton.SetActive(false);
                    qindex = 2;
                    event_text.text = "お";
                    CloneChoice();
                    break;
                case ("ch1_026"):
                    continueButton.SetActive(false);
                    qindex = 3;
                    event_text.text = "うえ";
                    CloneChoice();
                    break;
                case ("ch1_027"):
                    event_text.text = "あいうえお";
                    break;
                case ("ch1_033"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_034"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_035"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_036"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_037"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_042"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_043"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_044"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_045"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_046"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_047"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_048"):
                    continueButton.SetActive(false);
                    qindex = 4;
                    event_text.text = "き";
                    CloneChoice();
                    break;
                case ("ch1_049"):
                    continueButton.SetActive(false);
                    qindex = 5;
                    event_text.text = "し";
                    CloneChoice();
                    break;
                case ("ch1_050"):
                    continueButton.SetActive(false);
                    qindex = 6;
                    event_text.text = "「ko」";
                    CloneChoice();
                    break;
                case ("ch1_051"):
                    continueButton.SetActive(false);
                    qindex = 7;
                    event_text.text = "「sushi」";
                    CloneChoice();
                    break;
                case ("ch1_052"):
                    continueButton.SetActive(false);
                    qindex = 8;
                    event_text.text = "「かさ」";
                    CloneChoice();
                    break;
                case ("ch1_053"):
                    event_text.text = "かきくけこ\nさしすせそ";
                    break;
                case ("ch1_061"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_062"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_064"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_066"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_067"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_071"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_072"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_073"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_074"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_075"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_079"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_080"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_081"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_082"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_083"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_087"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_088"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_089"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_090"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_091"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_095"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_096"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_097"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_101"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_102"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_103"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_104"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_105"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_109"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_110"):
                    event_text.text = dialog.dialog_ch1[index].script_desc.Substring(1, 1);
                    break;
                case ("ch1_111"):
                    event_text.text = "ん";
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
        foreach (string s in dialog.question_ch1[qindex].question_choice)
        {
            CharObjectOfChapter clone = Instantiate(prefab.gameObject).GetComponent<CharObjectOfChapter>();
            clone.transform.SetParent(container);
            charObjectOfChapter.Add(clone.Init(s));
        }
    }

    public void CheckAnswer(CharObjectOfChapter charObjectOfChapters)
    {
        if (charObjectOfChapters.sentence == dialog.question_ch1[qindex].correct_answer)
        {
            charObjectOfChapter.Clear();
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }
            event_image.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

            textDisplays.text = "Very good!";
            event_text.text = "";
            speaker.text = dialog.dialog_ch1[index].script_role;
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
            speaker.text = dialog.dialog_ch1[index].script_role;
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
