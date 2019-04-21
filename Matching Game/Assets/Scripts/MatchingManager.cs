using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class MatchingManager : MonoBehaviour
{
    public Sprite carded;
    public Sprite card;
    public GameObject[] cards;
    public GameObject[] cards2;
    public GameObject[] cards1;
    public Text pointText;
    public Text timeText;
    public List<string> wordsListJP = new List<string>();
    public List<string> wordsListRJ = new List<string>();
    public string[] wordTest = new string[46];
    public string[] wordTest2 = new string[46];

    //ARRAY ที่เทสดึงมาจาก DB
    public string[] alphabetJP = new string[46];
    public string[] alphabetRomanji = new string[46];

    private float _timeLimit;
    private bool _init = false;
    private bool _initDB = false;
    private bool _initRAD = false;
    private int _point = 0;
    private int _correctWord;
    private int _wordIndex;
    private float _pointShow;

    private void Start()
    {
        StartCoroutine(TimeLimit());


    }

    void Update()
    {
        if (!_initDB)
        {
            PullWords();
        }
        if (_initDB)
        {
            RadWord();
        }
        if (!_init && _initDB && _initRAD)
        {
            initializeCard();
        }

        if (Input.GetMouseButtonUp(0))
        {
            checkCards();
        }

        _pointShow = Mathf.Lerp(_pointShow, _point, Time.deltaTime * 5);
        pointText.text = Mathf.RoundToInt(_pointShow).ToString();
    }

    void PullWords()
    {
        //ดึงมาจาก DB เอามาเก็บไว้ใน ARRAY สองตัวที่แอดมา 
        RestClient.GetArray<Alphabet>("https://it59-28yomimasu.firebaseio.com/Alphabet.json").Then(response =>
        {
            for (int i = 0; i <= 45; i++)
            {
                alphabetJP[i] = response[i].alphabetname_JP;
                alphabetRomanji[i] = response[i].alphabetname_romanji;
            }
        });
        if (alphabetRomanji[45] == "n")
        {
            _initDB = true;
        }
    }

    void RadWord()
    {
        for (int i = 0; i <= 46; i++)
        {

            _wordIndex = Random.Range(0, 12);
            if (!wordsListJP.Contains(alphabetJP[_wordIndex]))
            {
                wordsListJP.Add(alphabetJP[_wordIndex]);
                wordsListRJ.Add(alphabetRomanji[_wordIndex]);
            }
        }
        _initRAD = true;
    }

    //สุ่มค่าให้ปุ่ม กับกำหนด Text
    void initializeCard()
    {
        //for (int id = 0; id < 2; id++)
        //{

        //สุ่มปุ่มฝั่งซ้าย
        for (int i = 0; i < 12; i++)
        {
            bool test = false;
            int choice = 0;
            while (!test)
            {
                choice = Random.Range(0, cards.Length);
                test = !(cards[choice].GetComponent<Card>().initialized);
            }
            cards[choice].GetComponentInChildren<Text>().text = wordsListJP[i].ToString(); ;//i.ToString();
            cards[choice].GetComponent<Card>().cardValue = i;
            cards[choice].GetComponent<Card>().initialized = true;

        }

        //สุ่มปุ่มฝั่งขวา
        for (int i = 0; i < 12; i++)
        {
            bool test = false;
            int choice = 0;
            while (!test)
            {
                choice = Random.Range(0, cards1.Length);
                test = !(cards1[choice].GetComponent<Card>().initialized);
            }
            cards1[choice].GetComponentInChildren<Text>().text = wordsListRJ[i].ToString();//i.ToString();
            cards1[choice].GetComponent<Card>().cardValue = i;
            cards1[choice].GetComponent<Card>().initialized = true;

        }

        //}

        //เรียก Method ให้กดหนด Sprite ให้ปุ่ม ฝั่งซ้าย
        foreach (GameObject c in cards)
        {
            c.GetComponent<Card>().setupGraphics();
        }

        //เรียก Method ให้กดหนด Sprite ให้ปุ่ม ฝั่งขวา
        foreach (GameObject c in cards1)
        {
            c.GetComponent<Card>().setupGraphics();
        }

        //ทำการบอกว่าได้สุ่มค่าให้ปุ่มแล้ว
        if (!_init)
        {
            _init = true;
        }

    }

    //Method เรียก Sprite แบบยังไม่ได้กด
    public Sprite getCard()
    {
        return card;
    }

    //Method เรียก Sprite แบบกดแล้ว
    public Sprite getCarded()
    {
        return carded;
    }

    //Method เอาไว้ตรวจว่ากดปุ่มทั้ง 2 ปุ่มแล้วนำไปเทียบค่ากัน
    void checkCards()
    {
        List<int> t = new List<int>();

        for (int i = 0; i < cards2.Length; i++)
        {
            if (cards2[i].GetComponent<Card>().state == 1)
            {
                t.Add(i);
            }
        }

        if (t.Count == 2)
        {
            cardComparison(t);
        }
    }

    //Method เทียบค่าของปุ่มที่กดทั้ง 2 ปุ่ม
    void cardComparison(List<int> t)
    {
        Card.DO_NOT = true;

        int x = 0;
        if (cards2[t[0]].GetComponent<Card>().cardValue == cards2[t[1]].GetComponent<Card>().cardValue)
        {
            x = 2;
            _correctWord++;
            _point += Mathf.RoundToInt(_timeLimit);
            //point = Mathf.Lerp(point, _point, Time.deltaTime * 5);
            //pointText.text = Mathf.RoundToInt(point).ToString();
            if (_correctWord == 12)
            {
                SceneManager.LoadScene("MainMenu");
            }
            StartCoroutine(TimeLimit());
        }

        for (int i = 0; i < t.Count; i++)
        {
            cards2[t[i]].GetComponent<Card>().state = x;
            cards2[t[i]].GetComponent<Card>().falseCheck();
        }
    }

    IEnumerator TimeLimit()
    {
        _timeLimit = 15.0f;
        timeText.text = Mathf.RoundToInt(_timeLimit).ToString();

        int correct = _correctWord;

        yield return new WaitForSeconds(1f);
        while (_timeLimit > 0)
        {
            if (correct != _correctWord) { yield break; }

            _timeLimit -= Time.deltaTime;
            timeText.text = Mathf.RoundToInt(_timeLimit).ToString();
            yield return null;
        }

        //result.textTotalScore.text = result.totalScore.ToString();
    }
}
