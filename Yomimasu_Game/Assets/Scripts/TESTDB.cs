using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTDB : MonoBehaviour
{
    public string[] alphabetJP = new string[46];
    public string[] alphabetRomanji = new string[46];

    // Start is called before the first frame update
    void Start()
    {
        RestClient.GetArray<Alphabet>("https://it59-28yomimasu.firebaseio.com/Alphabet.json").Then(response =>
        {
            for (int i = 0; i <= 45; i++)
            {
                alphabetJP[i] = response[i].alphabetname_JP;
                alphabetRomanji[i] = response[i].alphabetname_romanji;

                Debug.Log(alphabetJP[i]);
                Debug.Log(alphabetRomanji[i]);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
