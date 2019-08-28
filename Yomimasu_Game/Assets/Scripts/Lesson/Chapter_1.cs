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
    public List<string> sentences = new List<string>();
    private int index;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject dialogBox;
    public GameObject teacherPic;
    public GameObject nameDisplay;
    public GameObject event_image;
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

    CharObjectOfChapter firstSelected;

    public static Chapter_1 main;

    // Start is called before the first frame update
    void Start()
    {
        //เรียกใช้แสดงประโยค
        foreach (RetrieveDialog item in DialogManager.dialog)
        {
            sentences.Add(item.script_desc);
        }
        speaker.text = DialogManager.dialog[index].script_role;
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        //ถ้าคำที่แสดง แสดงจนครบประโยคแล้วถึงกดไปต่อได้
        if (textDisplays.text == sentences[index])
        {
            continueButton.SetActive(true);
            RepositionObject();
        }

        if (DialogManager.dialog[index].script_role == "Teacher")
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
            speaker.text = DialogManager.dialog[index].script_role;
            //if (DialogManager.dialog[index].script_event == "1")
            //{
            //    _event.text = DialogManager.dialog[index].script_desc;
            //}
        }
        else
        {
            textDisplays.text = "";
            continueButton.SetActive(false);
            dialogBox.SetActive(false);
            nameDisplay.SetActive(false);
            teacherPic.SetActive(false);
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
        if (DialogManager.dialog[index].script_event == true)
        {
            switch (DialogManager.dialog[index].script_id)
            {
                default:
                    break;
                case ("ch1_013"):
                    event_text.text = "ひらがな";
                    //sprite_path = Resources.Load<Sprite>("Hiragana/a");
                    //event_image.GetComponent<Image>().sprite = sprite_path;
                    break;
                case ("ch1_014"):
                    event_text.text = "カタカナ";
                    break;
                case ("ch1_015"):
                    event_text.text = "漢字";
                    break;
                case ("ch1_019"):
                    sprite_path = Resources.Load<Sprite>("hiragana_chart");
                    EventImageCall();
                    break;
                case ("ch1_020"):
                    sprite_path = Resources.Load<Sprite>("dictionary");
                    EventImageCall();
                    break;
                case ("ch1_025"):
                    event_text.text = "あ";
                    break;
                case ("ch1_026"):
                    event_text.text = "い";
                    break;
                case ("ch1_027"):
                    event_text.text = "う";
                    break;
                case ("ch1_028"):
                    event_text.text = "え";
                    break;
                case ("ch1_029"):
                    event_text.text = "お";
                    break;
            }
        }
        //CloneChoice();
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
        /*charObjectOfChapter.Clear();
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }*/

        string[] sentence = sentences[index].ToString().Split();
        foreach (string s in sentence)
        {
            CharObjectOfChapter clone = Instantiate(prefab.gameObject).GetComponent<CharObjectOfChapter>();
            clone.transform.SetParent(container);
            charObjectOfChapter.Add(clone.Init(s));
        }
    }

    public void Select(CharObjectOfChapter charObjectOfChapter)
    {
        if (firstSelected)
        {
            firstSelected.Select();
            charObjectOfChapter.Select();

        }
        else
        {
            firstSelected = charObjectOfChapter;
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
}
