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

public class DialogManager
{
    public static List<RetrieveDialog> dialog = new List<RetrieveDialog>();
    public void PullDialog()
    {
        RestClient.GetArray<RetrieveDialog>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter1/Scripts.json").Then(response =>
        {
            for (int i = 0; i <= response.Length; i++)
            {
                dialog.Add(response[i]);
                //Debug.Log(dialog[i].script_id + " | " + dialog[i].script_role + " | " + dialog[i].script_desc + " | " + dialog[i].script_event);
            }
        });

        Debug.Log("Initial Dialog Complete!");
    }
}

public class QuestionManager
{
    public static List<RetrieveQuestion> question = new List<RetrieveQuestion>();
    public void PullQuestion()
    {
        RestClient.GetArray<RetrieveQuestion>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter1/Question.json").Then(response =>
        {
            for (int i = 0; i <= response.Length; i++)
            {
                question.Add(response[i]);
                Debug.Log(question[i].correct_answer + " | " + question[i].script_id);
                foreach (string item in question[i].question_choice)
                {
                    Debug.Log(item);
                }                
            }
        });

        Debug.Log("Initial Question Complete!");
    }
}
