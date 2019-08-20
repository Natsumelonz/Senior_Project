using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Proyecto26;

[Serializable]
public class RetrieveDialog
{
    public string script_id;
    public string script_role;
    public string script_desc;

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplays;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public static List<RetreiveDialog> dialog = new List<RetreiveDialog>();
    public GameObject continueButton;
    public GameObject dialogBox;

    //สร้างมาเพื่อให้ textDisplays ค่อยๆแสดงประโยคที่มีในarrayออกมา โดยใช้ typingSpeedเป็นตัวหน่วงเวลา
    IEnumerator Type()
    {
        dialogBox.SetActive(true);
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
        RestClient.GetArray<RetreiveDialog>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter1/Scripts.json").Then(response =>
        {
            for (int i = 0; i <= response.Length; i++)
            {
                dialog.Add(response[i]);
                Debug.Log(response[i]);
                
            }

            if (index < response.Length - 1)
            {
                index++;
                textDisplays.text = "";
                StartCoroutine(Type());
            }
            else
            {
                textDisplays.text = "";
                continueButton.SetActive(false);
                dialogBox.SetActive(false);

            }
        });
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
