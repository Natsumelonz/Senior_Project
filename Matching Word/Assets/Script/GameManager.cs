using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Sprite[] buttonSelect;
    public GameObject[] letters;
    public Text matchText;

    private bool _init = false;
    private int _matchses = 12;
    
    void Update()
    {
        if (!_init)
            initializeButtons();

        if (Input.GetMouseButtonUp(0))
            checkButtons();
    }
    void initializeButtons()
    {
        for (int id = 0; id < 2; id++)
        {
            for (int i = 1; i < 13 ; i++)
            {
                /*bool test = false;
                int choice = 0;
                while (!test)
                {
                    choice = Random.Range(0, letters.Length);
                    test = !(letters[choice].GetComponent<LetterMatching>().initilized);

                }*/
                letters[i].GetComponent<LetterMatching>().buttonValue = i;
                letters[i].GetComponent<LetterMatching>().initilized = true;

            }
        }
        foreach (GameObject l in letters)
            l.GetComponent<LetterMatching>().setupButton();
        if (!_init)
            _init = true;
    }

    public Sprite getButtonSelect(int i)
    {
        return buttonSelect[i - 1];
    }
    
    void checkButtons()
    {
        List<int> l = new List<int>();
        for (int i = 0; i < letters.Length; i++)
        {
                l.Add(i);
        }
        if (l.Count == 2)
            letterComparison(l);
    }

    void letterComparison(List<int> l)
    {
        LetterMatching.DO_NOT = true;

        int x = 0;
        if (letters[l[0]].GetComponent<LetterMatching>().buttonValue == letters[l[1]].GetComponent<LetterMatching>().buttonValue){
            x = 2;
            _matchses--;
            matchText.text = "Number of matches : " + _matchses;
            if (_matchses == 0)
            {

                SceneManager.LoadScene("MenuGame");
            }
        }

        for (int i = 0; i < l.Count; i++)
        {
            letters[l[i]].GetComponent<LetterMatching>().falseCheck();
        }
        
    }
    
}
