﻿using System.Collections;
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
    public string script_event_text;
    public string script_sprite_path;
    public bool script_question;

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

[Serializable]
public class RetrieveTest
{
    public int test_answer;
    public List<string> test_choice = new List<string>();
    public string test_desc;
    public string test_id;

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}

[Serializable]
public class ContentInfo
{
    public string Name;
    public string Objective;
    public List<RetrieveQuestion> Questions;
    public List<RetrieveDialog> Scripts;
    public List<RetrieveTest> Tests;
    public int chap_id;

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}

public class ContentManager : MonoBehaviour
{
    public static ContentManager Instance { set; get; }
    public ContentInfo chapter1_info = new ContentInfo();
    public ContentInfo chapter2_info = new ContentInfo();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        PullContentInfo();
    }

    public void PullContentInfo()
    {
        RestClient.Get<ContentInfo>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter1.json").Then(response =>
        {
            chapter1_info = response; 
            //Debug.Log(chapter1_info.Name + " | " + chapter1_info.Objective);
        });
        
        RestClient.Get<ContentInfo>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter2.json").Then(response =>
        {
            chapter2_info = response; 
            //Debug.Log(chapter2_info.Name + " | " + chapter2_info.Objective);
        });

        Debug.Log("Initial ContentInfo Complete!");
    }
}