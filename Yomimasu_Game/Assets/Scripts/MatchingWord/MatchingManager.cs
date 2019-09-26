using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Proyecto26;

[System.Serializable]
public class UIObject
{
    [Header("Text")]
    public Text pointText;
    public Text timeText;
    public Text textSummaryScore;
    public Text textInfo;
    public Text ScorePop;
    public Text LevelText;
    public Text scorePause;
    public GameObject summaryCanvas;
    [Header("Button")]
    public GameObject[] buttoneLeft;
    public GameObject[] buttoneRight;
    public GameObject[] allButton;
    public GameObject pauseTab;
}

[System.Serializable]
public class MatchingManager : MonoBehaviour
{
    public AudioClip effectClip;
    public AudioSource effectAudio;

    [Header("UIObject")]
    public UIObject uiObject = new UIObject();

    [Header("Alphabets")]
    public List<string> alphabetListJP = new List<string>();
    public List<string> alphabetListRJ = new List<string>();

    [Header("Sprite")]
    public Sprite carded;
    public Sprite card;

    [Header("Private Attribute")]
    private int _point = 0;
    private int level = 1;
    private float _timeLimit = 15f;
    private bool _init = false;
    //private bool _initDB = false;
    private bool _initRAD = false;
    private int _correctWord = 0;
    private int _wordIndex = 0;
    private float _pointShow;
    private int disabledHash = Animator.StringToHash("Disabled");
    private GameObject Manager;
    private GameObject Audio;
    private GameObject Effect;

    private void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        Audio = GameObject.Find("AudioManager").gameObject;
        Effect = GameObject.Find("EffectManager").gameObject;
        effectAudio.clip = effectClip;
        StartCoroutine(RadWord(0f, level));
        uiObject.LevelText.text = level.ToString();

        uiObject.summaryCanvas.SetActive(false);

        foreach (GameObject c in uiObject.allButton)
        {
            c.GetComponent<Card>().initialized = false;
            c.GetComponent<Card>().state = 0;
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
        uiObject.pointText.text = Mathf.RoundToInt(_pointShow).ToString();
    }

    IEnumerator RadWord(float Time, int level)
    {
        yield return new WaitForSeconds(Time);

        if (level < 6)
        {
            while (alphabetListJP.Count < 12)
            {
                _wordIndex = Random.Range(0, 46);
                if (!alphabetListJP.Contains(Manager.GetComponent<AlpabetManager>().alphabets[_wordIndex].alphabetname_JP))
                {
                    alphabetListJP.Add(Manager.GetComponent<AlpabetManager>().alphabets[_wordIndex].alphabetname_JP);
                    alphabetListRJ.Add(Manager.GetComponent<AlpabetManager>().alphabets[_wordIndex].alphabetname_romanji);
                }
            }
        }
        else if (level < 11)
        {
            while (alphabetListJP.Count < 12)
            {
                _wordIndex = Random.Range(46, 104);
                if (!alphabetListJP.Contains(Manager.GetComponent<AlpabetManager>().alphabets[_wordIndex].alphabetname_JP))
                {
                    alphabetListJP.Add(Manager.GetComponent<AlpabetManager>().alphabets[_wordIndex].alphabetname_JP);
                    alphabetListRJ.Add(Manager.GetComponent<AlpabetManager>().alphabets[_wordIndex].alphabetname_romanji);
                }
            }
        }
        else
        {
            while (alphabetListJP.Count < 12)
            {
                _wordIndex = Random.Range(0, 104);
                if (!alphabetListJP.Contains(Manager.GetComponent<AlpabetManager>().alphabets[_wordIndex].alphabetname_JP))
                {
                    alphabetListJP.Add(Manager.GetComponent<AlpabetManager>().alphabets[_wordIndex].alphabetname_JP);
                    alphabetListRJ.Add(Manager.GetComponent<AlpabetManager>().alphabets[_wordIndex].alphabetname_romanji);
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
                choice = Random.Range(0, uiObject.buttoneLeft.Length);
                test = !(uiObject.buttoneLeft[choice].GetComponent<Card>().initialized);
            }
            uiObject.buttoneLeft[choice].GetComponentInChildren<Text>().text = alphabetListJP[i].ToString(); ;//i.ToString();
            uiObject.buttoneLeft[choice].GetComponent<Card>().cardValue = i;
            uiObject.buttoneLeft[choice].GetComponent<Card>().initialized = true;

        }

        //สุ่มปุ่มฝั่งขวา
        for (int i = 0; i < 12; i++)
        {
            bool test = false;
            int choice = 0;
            while (!test)
            {
                choice = Random.Range(0, uiObject.buttoneRight.Length);
                test = !(uiObject.buttoneRight[choice].GetComponent<Card>().initialized);
            }
            uiObject.buttoneRight[choice].GetComponentInChildren<Text>().text = alphabetListRJ[i].ToString();//i.ToString();
            uiObject.buttoneRight[choice].GetComponent<Card>().cardValue = i;
            uiObject.buttoneRight[choice].GetComponent<Card>().initialized = true;

        }

        //เรียก Method ให้กดหนด Sprite ให้ปุ่ม ฝั่งซ้าย
        foreach (GameObject c in uiObject.allButton)
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

        for (int i = 0; i < uiObject.allButton.Length; i++)
        {
            if (uiObject.allButton[i].GetComponent<Card>().state == 1)
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
        if (uiObject.allButton[t[0]].GetComponent<Card>().cardValue == uiObject.allButton[t[1]].GetComponent<Card>().cardValue)
        {
            x = 2;

            _correctWord++;
            _point += Mathf.RoundToInt(_timeLimit);
            //Debug.Log("Correct Word: " + _correctWord);
            if (_correctWord % 12 == 0)
            {
                StartCoroutine(Next(1f));
            }
            StartCoroutine(TimeLimit());
            StartCoroutine(Scored("Correct!!", 1.5f));
            for (int i = 0; i < t.Count; i++)
            {
                uiObject.allButton[t[i]].GetComponent<Button>().interactable = false;
                uiObject.allButton[t[i]].GetComponent<Animator>().SetTrigger(disabledHash);
            }
        }

        for (int i = 0; i < t.Count; i++)
        {
            uiObject.allButton[t[i]].GetComponent<Card>().state = x;
            uiObject.allButton[t[i]].GetComponent<Card>().falseCheck();
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
        foreach (GameObject item in uiObject.allButton)
        {
            item.GetComponent<Button>().interactable = false;
        }

        uiObject.textSummaryScore.text = _point.ToString();
        uiObject.textInfo.text = "You finished " + _correctWord + " pairs";

        uiObject.summaryCanvas.SetActive(true);
    }

    public void Skip()
    {
        level++; Debug.Log("Level: " + level);
        SceneManager.LoadScene("GameMatching");
    }

    public IEnumerator Next(float Time)
    {
        yield return new WaitForSeconds(Time);
        level++; //Debug.Log("Level: " + level);
        SceneManager.LoadScene("GameMatching");
    }

    IEnumerator TimeLimit()
    {
        _timeLimit = 15.0f;
        uiObject.timeText.text = Mathf.RoundToInt(_timeLimit).ToString();

        int correct = _correctWord;

        yield return new WaitForSeconds(1f);
        while (_timeLimit > 0)
        {
            if (correct != _correctWord) { yield break; }

            _timeLimit -= Time.deltaTime;
            uiObject.timeText.text = Mathf.RoundToInt(_timeLimit).ToString();
            yield return null;
        }

        //result.textTotalScore.text = result.totalScore.ToString();
    }

    IEnumerator Scored(string message, float delay)
    {
        uiObject.ScorePop.text = message;
        uiObject.ScorePop.enabled = true;
        effectAudio.Play();
        yield return new WaitForSeconds(delay);
        uiObject.ScorePop.enabled = false;
    }

    public void PauseButton()
    {
        Effect.GetComponent<AudioSource>().Play();
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            uiObject.pauseTab.SetActive(true);
            uiObject.scorePause.text = _point.ToString();
        }
        else
        {
            Time.timeScale = 1;
            uiObject.pauseTab.SetActive(false);
        }
    }

    public void PauseMenu(int i)
    {
        Time.timeScale = 1;
        switch (i)
        {
            default:
                break;
            case (0):
                level = 1;

                if (Manager.GetComponent<UserManager>().user.Score1 < _point)
                {
                    Manager.GetComponent<UserManager>().user.Score1 = _point;
                }

                Manager.GetComponent<UserManager>().Save();
                HighScores.AddNewHighscore1(Manager.GetComponent<UserManager>().user.Name, Manager.GetComponent<UserManager>().user.Score1);
                Effect.GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("MainMenu");
                break;
            case (1):
                level = 1;

                if (Manager.GetComponent<UserManager>().user.Score1 < _point)
                {
                    Manager.GetComponent<UserManager>().user.Score1 = _point;
                }

                Manager.GetComponent<UserManager>().Save();
                HighScores.AddNewHighscore1(Manager.GetComponent<UserManager>().user.Name, Manager.GetComponent<UserManager>().user.Score1);
                Effect.GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("GameMatching");
                break;
            case (2):
                level = 1;

                if (Manager.GetComponent<UserManager>().user.Score1 < _point)
                {
                    Manager.GetComponent<UserManager>().user.Score1 = _point;
                }

                Manager.GetComponent<UserManager>().Save();
                HighScores.AddNewHighscore1(Manager.GetComponent<UserManager>().user.Name, Manager.GetComponent<UserManager>().user.Score1);
                Effect.GetComponent<AudioSource>().Play();
                Audio.GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("GameMenu");
                break;
        }
    }
}
