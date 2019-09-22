using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Proyecto26;

[Serializable]
public class RetrieveDialog
{
    public string script_id;
    public string script_role;
    public string script_desc;
    public bool script_event;

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}

[Serializable]
public class RetrieveQuestion
{
    public string correct_answer;
    public List<string> question_choice;
    public string script_id;

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}

public class ChapterManager : MonoBehaviour
{
    public static ChapterManager Instance { set; get; }
    public List<RetrieveDialog> dialog_ch1 = new List<RetrieveDialog>();
    public List<RetrieveDialog> dialog_ch2 = new List<RetrieveDialog>();
    public List<RetrieveQuestion> question_ch1 = new List<RetrieveQuestion>();
    public List<RetrieveQuestion> question_ch2 = new List<RetrieveQuestion>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        PullDialog();
        PullQuestion();
    }

    public void PullDialog()
    {
        RestClient.GetArray<RetrieveDialog>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter1/Scripts.json").Then(response =>
        {
            for (int i = 0; i <= response.Length; i++)
            {
                dialog_ch1.Add(response[i]);
                //Debug.Log(dialog[i].script_id + " | " + dialog[i].script_role + " | " + dialog[i].script_desc + " | " + dialog[i].script_event);
            }
        });

        Debug.Log("Initial Dialog Ch1 Complete!");

        RestClient.GetArray<RetrieveDialog>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter2/Scripts.json").Then(response =>
        {
            for (int i = 0; i <= response.Length; i++)
            {
                dialog_ch2.Add(response[i]);
                //Debug.Log(dialog[i].script_id + " | " + dialog[i].script_role + " | " + dialog[i].script_desc + " | " + dialog[i].script_event);
            }
        });

        Debug.Log("Initial Dialog Ch2 Complete!");
    }


    public void PullQuestion()
    {
        RestClient.GetArray<RetrieveQuestion>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter1/Questions.json").Then(response =>
        {
            for (int i = 0; i <= response.Length; i++)
            {
                question_ch1.Add(response[i]);
                //Debug.Log(question[i].correct_answer + " | " + question[i].script_id);
                //foreach (string item in question[i].question_choice)
                //{
                //    Debug.Log(item);
                //}                
            }
        });

        Debug.Log("Initial Question Ch1 Complete!");

        RestClient.GetArray<RetrieveQuestion>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter2/Questions.json").Then(response =>
        {
            for (int i = 0; i <= response.Length; i++)
            {
                question_ch2.Add(response[i]);
                //Debug.Log(question[i].correct_answer + " | " + question[i].script_id);
                //foreach (string item in question[i].question_choice)
                //{
                //    Debug.Log(item);
                //}                
            }
        });

        Debug.Log("Initial Question Ch2 Complete!");
    }
}
