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
    public Text _event;
    public Text speaker;
    public Text textDisplays;

    public GameObject ChapterCanvas;
    public CharObjectOfChapter prefab;

    //ไว้เก็บbutton ที่clone มา
    public Transform container;
    public float space;
    public float lerpSpeed = 5;
    List<CharObjectOfChapter> charObjectOfChapter = new List<CharObjectOfChapter>();

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

    public void NextSentence()
    {
        //ปิดปุ่มContinue
        continueButton.SetActive(false);
        //ถ้าindexประโยคยังไม่หมดทำต่อ       

        if (index < sentences.Count - 1)
        {
            index++;
            textDisplays.text = "";
            _event.text = "";
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
    }

    void RepositionObject()
    {

        if (charObjectOfChapter.Count == 0)
        {
            return;
        }
        float center = (charObjectOfChapter.Count - 1f) / 2;
        for (int i = 0; i < charObjectOfChapter.Count; i++)
        {
            charObjectOfChapter[i].rectTransform.anchoredPosition
                = Vector2.Lerp(charObjectOfChapter[i].rectTransform.anchoredPosition,
                 new Vector2((i - center) * space, 0), lerpSpeed * Time.deltaTime);
            charObjectOfChapter[i].index = i;
        }
    }
    void Awake()
    {
        main = this;
    }

    void CloneChoice()
    {
        charObjectOfChapter.Clear();
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

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
    // Update is called once per frame
    void Update()
    {
        //ถ้าคำที่แสดง แสดงจนครบประโยคแล้วถึงกดไปต่อได้
        if (textDisplays.text == sentences[index])
        {
            continueButton.SetActive(true);
            CloneChoice();
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
}
