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
    public List<Alphabet> alphabetsHR = new List<Alphabet>();
    public List<Alphabet> alphabetsKT = new List<Alphabet>();

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
            int id;
            for (int i = 0; i <= response.Length; i++)
            {
                id = Int32.Parse(response[i].alphabet_id);
                if (id <= 104)
                {
                    alphabetsHR.Add(response[i]);
                }
                else
                {
                    alphabetsKT.Add(response[i]);
                }
                //Debug.Log(i);
                //Debug.Log(alphabets[i].alphabetname_JP);
                //Debug.Log(alphabets[i].alphabetname_romanji);
            }
        });

        Debug.Log("Initial Alphabets Complete!");
    }
}
