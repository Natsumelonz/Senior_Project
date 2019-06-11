using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplays;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;

    //สร้างมาเพื่อให้ textDisplays ค่อยๆแสดงประโยคที่มีในarrayออกมา โดยใช้ typingSpeedเป็นตัวหน่วงเวลา
    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplays.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //เรียกใช้แสดงประโยค
        StartCoroutine(Type());
    }

    public void NextSentence()
    {
        //ปิดปุ่มContinue
        continueButton.SetActive(false);
        //ถ้าindexประโยคยังไม่หมดทำต่อ
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplays.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplays.text = "";
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ถ้าคำที่แสดง แสดงจนครบประโยคแล้วถึงกดไปต่อได้
        if(textDisplays.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }
}
