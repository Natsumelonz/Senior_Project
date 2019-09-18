using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Proyecto26;

[Serializable]
public class Alphabet
{
    public string alphabet_id;
    public string alphabetname_JP;
    public string alphabetname_romanji;    

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }   
    
}

[Serializable]
public class AlpabetManager : MonoBehaviour
{
    public static AlpabetManager Instance { set; get; }
    public List<Alphabet> alphabets = new List<Alphabet>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        PullAlphabets();
    }

    public void PullAlphabets()
    {
        //ดึงมาจาก DB เอามาเก็บไว้ใน ARRAY สองตัวที่แอดมา 
        RestClient.GetArray<Alphabet>("https://it59-28yomimasu.firebaseio.com/Alphabet.json").Then(response =>
        {
            for (int i = 0; i <= 103; i++)
            {
                alphabets.Add(response[i]);
                //Debug.Log(i);
                //Debug.Log(alphabets[i].alphabetname_JP);
                //Debug.Log(alphabets[i].alphabetname_romanji);
            }
        });

        Debug.Log("Initial Alphabets Complete!");
    }
}
