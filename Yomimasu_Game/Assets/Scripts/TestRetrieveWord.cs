using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class TestRetrieveWord : MonoBehaviour
{
    public string[] word = new string[100];
    // Start is called before the first frame update
    void Start()
    {
        RestClient.GetArray<RetreiveWord>("https://it59-28yomimasu.firebaseio.com/Word.json").Then(response =>
        {
            for (int i = 0; i <= 20; i++)
            {
                Debug.Log(response[i].wordname_JP);
                word[i] = response[i].wordname_JP;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
