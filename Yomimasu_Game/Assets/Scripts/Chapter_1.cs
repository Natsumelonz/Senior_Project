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

    // Update is called once per frame
    void Update()
    {
        //ถ้าคำที่แสดง แสดงจนครบประโยคแล้วถึงกดไปต่อได้
        if (textDisplays.text == sentences[index])
        {
            continueButton.SetActive(true);
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
