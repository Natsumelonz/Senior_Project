using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Proyecto26;

[Serializable]
public class Words
{
    public int word_id;
    public string wordname_JP;
    public string wordname_romanji;
    public string word_meaning;
    public int word_syllable;

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}

public class WordManager : MonoBehaviour
{
    public static WordManager Instance { set; get; }
    public List<Words> words = new List<Words>();
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        PullWords();
    }

    public void PullWords()
    {
        //ดึงมาจาก DB เอามาเก็บไว้ใน ARRAY สองตัวที่แอดมา 
        RestClient.GetArray<Words>("https://it59-28yomimasu.firebaseio.com/Word.json").Then(response =>
        {
            for (int i = 0; i <= response.Length; i++)
            {
                words.Add(response[i]);
                //Debug.Log(i);
                //Debug.Log(words[i].wordname_JP);
                //Debug.Log(words[i].wordname_romanji);
            }
        });

        Debug.Log("Initial Words Complete!");
    }
}