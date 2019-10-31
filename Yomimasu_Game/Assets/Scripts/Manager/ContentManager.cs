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
    public string test_sprite_path;
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
    public string chap_avg_time;

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}

public class ContentManager : MonoBehaviour
{
    public static ContentManager Instance { set; get; }
    public ContentInfo[] chapters_info = new ContentInfo[8];

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
            chapters_info[0] = response;
        });
        
        RestClient.Get<ContentInfo>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter2.json").Then(response =>
        {
            chapters_info[1] = response;
        });

        RestClient.Get<ContentInfo>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter3.json").Then(response =>
        {
            chapters_info[2] = response;
        });
        
        RestClient.Get<ContentInfo>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter4.json").Then(response =>
        {
            chapters_info[3] = response;
        });

        RestClient.Get<ContentInfo>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter5.json").Then(response =>
        {
            chapters_info[4] = response;
        });

        RestClient.Get<ContentInfo>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter6.json").Then(response =>
        {
            chapters_info[5] = response;
        });

        RestClient.Get<ContentInfo>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter7.json").Then(response =>
        {
            chapters_info[6] = response;
        });

        RestClient.Get<ContentInfo>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter8.json").Then(response =>
        {
            chapters_info[7] = response;
        });

        Debug.Log("Initial ContentInfo Complete!");
    }
}
