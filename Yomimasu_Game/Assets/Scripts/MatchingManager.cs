using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Proyecto26;

[System.Serializable]
public class MatchingManager : MonoBehaviour
{
    [Header("Alphabets")]
    public List<string> alphabetListJP = new List<string>();
    public List<string> alphabetListRJ = new List<string>();

    [Header("Sprite")]
    public Sprite carded;
    public Sprite card;

    [Header("Button")]
    public GameObject[] buttoneLeft;
    public GameObject[] buttoneRight;
    public GameObject[] allButton;

    [Header("Text")]
    public Text pointText;
    public Text timeText;
    public Text textSummaryScore;
    public Text textInfo;
    public Text ScorePop;
    public GameObject summaryCanvas;

    private int _point = 0;
    private int level = 1;
    private float _timeLimit = 15f;
    private bool _init = false;
    //private bool _initDB = false;
    private bool _initRAD = false;
    private int _correctWord = 0;
    private int _wordIndex = 0;
    private float _pointShow;

    private void Start()
    {
        StartCoroutine(RadWord(0f, level));

        summaryCanvas.SetActive(false);

        for (int i = 0; i < allButton.Length; i++)
        {
            allButton[i].GetComponent<Card>().initialized = false;
            allButton[i].GetComponent<Card>().state = 0;
        }

    }

    void Update()
    {
        if (!_init && _initRAD)
        {
            InitializeCard();
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

    IEnumerator RadWord(float Time, int level)
    {
        yield return new WaitForSeconds(Time);

        if (level < 6)
        {
            while (alphabetListJP.Count < 12)
            {
                _wordIndex = Random.Range(0, 46);
                if (!alphabetListJP.Contains(MenuBehaviour.alphabets[_wordIndex].alphabetname_JP))
                {
                    alphabetListJP.Add(MenuBehaviour.alphabets[_wordIndex].alphabetname_JP); 
                    alphabetListRJ.Add(MenuBehaviour.alphabets[_wordIndex].alphabetname_romanji);
                }
            }            
        }
        else if (level < 11)
        {
            while (alphabetListJP.Count < 12)
            {
                _wordIndex = Random.Range(46, 104);
                if (!alphabetListJP.Contains(MenuBehaviour.alphabets[_wordIndex].alphabetname_JP))
                {
                    alphabetListJP.Add(MenuBehaviour.alphabets[_wordIndex].alphabetname_JP);
                    alphabetListRJ.Add(MenuBehaviour.alphabets[_wordIndex].alphabetname_romanji);
                }
            }
        }
        else{
            while (alphabetListJP.Count < 12)
            {
                _wordIndex = Random.Range(0, 104);
                if (!alphabetListJP.Contains(MenuBehaviour.alphabets[_wordIndex].alphabetname_JP))
                {
                    alphabetListJP.Add(MenuBehaviour.alphabets[_wordIndex].alphabetname_JP);
                    alphabetListRJ.Add(MenuBehaviour.alphabets[_wordIndex].alphabetname_romanji);
                }
            }
        }

        //foreach (string item in alphabetListJP)
        //{
        //    Debug.Log(item);
        //}
        //foreach (string item in alphabetListRJ)
        //{
        //    Debug.Log(item);
        //}

        if (alphabetListJP.Count == 12)
        {
            _initRAD = true;
        }

    }

    //สุ่มค่าให้ปุ่ม กับกำหนด Text
    void InitializeCard()
    {
        //สุ่มปุ่มฝั่งซ้าย

        for (int i = 0; i < 12; i++)
        {
            bool test = false;
            int choice = 0;
            while (!test)
            {
                choice = Random.Range(0, buttoneLeft.Length);
                test = !(buttoneLeft[choice].GetComponent<Card>().initialized);
            }
            buttoneLeft[choice].GetComponentInChildren<Text>().text = alphabetListJP[i].ToString(); ;//i.ToString();
            buttoneLeft[choice].GetComponent<Card>().cardValue = i;
            buttoneLeft[choice].GetComponent<Card>().initialized = true;

        }

        //สุ่มปุ่มฝั่งขวา
        for (int i = 0; i < 12; i++)
        {
            bool test = false;
            int choice = 0;
            while (!test)
            {
                choice = Random.Range(0, buttoneRight.Length);
                test = !(buttoneRight[choice].GetComponent<Card>().initialized);
            }
            buttoneRight[choice].GetComponentInChildren<Text>().text = alphabetListRJ[i].ToString();//i.ToString();
            buttoneRight[choice].GetComponent<Card>().cardValue = i;
            buttoneRight[choice].GetComponent<Card>().initialized = true;

        }

        //เรียก Method ให้กดหนด Sprite ให้ปุ่ม ฝั่งซ้าย
        foreach (GameObject c in allButton)
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

        for (int i = 0; i < allButton.Length; i++)
        {
            if (allButton[i].GetComponent<Card>().state == 1)
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
        if (allButton[t[0]].GetComponent<Card>().cardValue == allButton[t[1]].GetComponent<Card>().cardValue)
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
            StartCoroutine(Scored("Correct!!", 1.5f));
            for (int i = 0; i < t.Count; i++)
            {
                allButton[t[i]].GetComponent<Button>().interactable = false;
            }
        }

        for (int i = 0; i < t.Count; i++)
        {
            allButton[t[i]].GetComponent<Card>().state = x;
            allButton[t[i]].GetComponent<Card>().falseCheck();
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
        foreach (GameObject item in allButton)
        {
            item.GetComponent<Button>().interactable = false;
        }

        textSummaryScore.text = _point.ToString();
        textInfo.text = "You finished " + _correctWord + " pairs";

        summaryCanvas.SetActive(true);
    }

    public void Skip()
    {
        level++; Debug.Log("Level: " + level);
        SceneManager.LoadScene("GameMatching");
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

    IEnumerator Scored(string message, float delay)
    {
        ScorePop.text = message;
        ScorePop.enabled = true;
        yield return new WaitForSeconds(delay);
        ScorePop.enabled = false;
    }
}
