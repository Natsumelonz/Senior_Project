using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Proyecto26;

[System.Serializable]
public class Alphabets
{
    public List<string> alphabetJP = new List<string>();
    public List<string> alphabetRomanji = new List<string>();
}

public class MatchingManager : MonoBehaviour
{
    [Header("Alphabets")]
    public Alphabets alphabet;
    public List<string> alphabetListJP = new List<string>();
    public List<string> alphabetListRJ = new List<string>();

    [Header("Sprite")]
    public Sprite carded;
    public Sprite card;

    [Header("Button")]
    public GameObject[] cards;
    public GameObject[] cards1;
    public GameObject[] cards2;

    [Header("Text")]
    public Text pointText;
    public Text timeText;
    public Text textSummaryScore;
    public Text textInfo;
    public GameObject summaryCanvas;

    private int _point = 0;
    private int level = 1;
    private float _timeLimit = 15f;
    private bool _init = false;
    //private bool _initDB = false;
    private bool _initRAD = false;
    private int _correctWord = 0;
    private int _wordIndex;
    private float _pointShow;

    private void Start()
    {
        PullWords(); Debug.Log("Hello DB");
        StartCoroutine(RadWord(1.5f));

        summaryCanvas.SetActive(false);

        for (int i = 0; i < cards2.Length; i++)
        {
            cards2[i].GetComponent<Card>().initialized = false;
            cards2[i].GetComponent<Card>().state = 0;
        }

    }

    void Update()
    {
        //if (!_initDB && alphabetJP.Count == 0 && alphabetJP.Count <= 46)
        //{
        //    PullWords(); Debug.Log("Hello DB");
        //}

        //if (_initDB && !_initRAD)
        //{
        //    RadWord();
        //}

        //if (!_init && _initDB && _initRAD)
        if (!_init && _initRAD)
        {
            initializeCard();
        }

        if (Input.GetMouseButtonUp(0))
        {
            checkCards();
        }

        if (_timeLimit <= 0)
        {
            ShowSummary();
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
                alphabet.alphabetJP.Add(response[i].alphabetname_JP);
                alphabet.alphabetRomanji.Add(response[i].alphabetname_romanji);
            }
        });
        //if (alphabetRomanji.Count == 46)
        //{
        //    _initDB = true;
        //}
    }

    IEnumerator RadWord(float Time)
    {
        yield return new WaitForSeconds(Time);
        while (alphabetListJP.Count < 12)
        {
            _wordIndex = Random.Range(0, 46);
            if (!alphabetListJP.Contains(alphabet.alphabetJP[_wordIndex]))
            {
                alphabetListJP.Add(alphabet.alphabetJP[_wordIndex]);
                alphabetListRJ.Add(alphabet.alphabetRomanji[_wordIndex]);
            }
        }

        if (alphabetListJP.Count == 12)
        {
            _initRAD = true;
        }
    }

    //สุ่มค่าให้ปุ่ม กับกำหนด Text
    void initializeCard()
    {
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
            cards[choice].GetComponentInChildren<Text>().text = alphabetListJP[i].ToString(); ;//i.ToString();
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
            cards1[choice].GetComponentInChildren<Text>().text = alphabetListRJ[i].ToString();//i.ToString();
            cards1[choice].GetComponent<Card>().cardValue = i;
            cards1[choice].GetComponent<Card>().initialized = true;

        }

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
            StartCoroutine(TimeLimit());
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
            Debug.Log("Correct Word: " + _correctWord);
            if (_correctWord % 12 == 0)
            {
                StartCoroutine(Next(1f));
            }
            StartCoroutine(TimeLimit());
        }

        for (int i = 0; i < t.Count; i++)
        {
            cards2[t[i]].GetComponent<Card>().state = x;
            cards2[t[i]].GetComponent<Card>().falseCheck();
        }
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("point", _point);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("correctword", _correctWord);
    }

    private void OnEnable()
    {
        //if (_correctWord == 0)
        //{
        level = PlayerPrefs.GetInt("level");
        //}

        if (level > 1)
        {
            _point = PlayerPrefs.GetInt("point");
            _correctWord = PlayerPrefs.GetInt("correctword");
        }
    }

    void ShowSummary()
    {
        foreach (GameObject item in cards2)
        {
            item.GetComponent<Button>().interactable = false;
        }

        textSummaryScore.text = _point.ToString();
        textInfo.text = "You finished " + _correctWord + " pairs";

        summaryCanvas.SetActive(true);
    }

    public IEnumerator Next(float Time)
    {
        yield return new WaitForSeconds(Time);
        level++; Debug.Log("Level: " + level);
        SceneManager.LoadScene("GameMatching");
    }

    public void Replay()
    {
        level = 1;
        SceneManager.LoadScene("GameMatching");
    }
    public void MainMenu()
    {
        level = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void Back()
    {
        level = 1;
        SceneManager.LoadScene("MainMenu");
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
