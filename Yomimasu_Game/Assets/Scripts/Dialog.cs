using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using Proyecto26;

public class Dialog : MonoBehaviour
{
    public List<string> sentences = new List<string>();
    private int index;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject dialogBox;
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
            StartCoroutine(Type());
            speaker.text = DialogManager.dialog[index].script_role;
        }
        else
        {
            textDisplays.text = "";
            continueButton.SetActive(false);
            dialogBox.SetActive(false);
        }
    }

    //สร้างมาเพื่อให้ textDisplays ค่อยๆแสดงประโยคที่มีในarrayออกมา โดยใช้ typingSpeedเป็นตัวหน่วงเวลา
    IEnumerator Type()
    {
        dialogBox.SetActive(true);
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
    }
}
