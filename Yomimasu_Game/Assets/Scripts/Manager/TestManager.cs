using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Proyecto26;

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

public class TestManager : MonoBehaviour
{
    public static TestManager Instance { set; get; }
    public List<RetrieveTest> testCh1 = new List<RetrieveTest>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        PullTest();
    }

    public void PullTest()
    {
        RestClient.GetArray<RetrieveTest>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter1/Tests.json").Then(response =>
        {
            for (int i = 0; i <= response.Length; i++)
            {
                testCh1.Add(response[i]);
                //Debug.Log(testCh1[i].test_id + " | " + testCh1[i].test_desc);
            }
        });

        Debug.Log("Initial Test Ch1 Complete!");
    }
}
