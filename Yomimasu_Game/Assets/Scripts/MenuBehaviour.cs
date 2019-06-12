using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Proyecto26;

public class MenuBehaviour : MonoBehaviour
{

    public static List<Alphabet> alphabets = new List<Alphabet>();

    void OnDisable()
    {
        PlayerPrefs.SetInt("level", 1);
    }

    private void Start()
    {
        PullWords();
    }

    private void Update()
    {
        
    }

    void PullWords()
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

    public void TriggerMenuBehaviour(int i)
    {
        switch (i)
        {
            default:
                break;
            case (0):
                Application.Quit();
                break;
            case (1):
                SceneManager.LoadScene("GameMatching");
                break;
            case (2):
                SceneManager.LoadScene("SortingWord");
                break;
        }
    }
}
