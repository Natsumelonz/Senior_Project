using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Proyecto26;

[System.Serializable]
public class Result
{
    public int totalScore = 0;

    [Header("REF UI")]
    public Text textTime;
    public Text textTotalScore;
    public Text textJapan;
    public Text textMean;
    public Text scorePause;

    [Header("REF RESULT SCREEN")]
    public GameObject summaryCanvas;
    public Text textSummaryScore;
    public Text textInfo;

    public void ShowSummary()
    {
        textSummaryScore.text = totalScore.ToString();
        textInfo.text = "You finished " + WordSorting.main.countWords.ToString() + " words";

        summaryCanvas.SetActive(true);
    }

}


[System.Serializable]
public class Word
{
    //มีคำอะไรบ้างเก็บเป็น array
    public string word;

    public string GetString()
    {
        //ถ้า desiredRandom ไม่เป็นค่าว่าง ให้มันreturn ตัวมันออกมา คือget ค่ามันมา
        //if (!string.IsNullOrEmpty(desiredRandom))
        //{
        //    return desiredRandom;
        //}
        //เก็บ word เข้าไปแสดง
        string result = word;

        while (result.Equals(word))
        {
            result = "";
            //เก็บคำไว้ใน list เพื่อจะเอามาสุ่มตัวอักษร
            List<char> characters = new List<char>(word.ToCharArray());
            while (characters.Count > 0)
            {
                //ไว้สำหรับแรนด้อมตัวอักษรในคำ ก่อนที่จะทำการเรียง
                int indexChar = Random.Range(0, characters.Count - 1);

                //เอาตำแหน่งที่แรมด้อมแล้วไปเก็บใน result เพื่อให้ user เห็นก่อนเรียง
                result += characters[indexChar];

                //เอา index ที่ใส่ไว้ใน result ออกทีละตัว ให้ตัวถัดไปมาใส่ใน result
                characters.RemoveAt(indexChar);

            }
        }
        return result;
    }
}

public class WordSorting : MonoBehaviour
{
    //เรียกclass word เก็บเป็น size Array แล้วเด้งไปคลาส word ก่อน
    public List<Word> words;

    //ใช้นับคำที่ผู้เล่นตอบถูก
    [Space(10)]
    public int countWords = 0;

    [Space(15)]
    public Result result;

    [Header("UI REFERENCE")]
    public GameObject wordCanvas;
    public GameObject pauseTab;
    public CharObject prefab;

    //ไว้เก็บbutton ที่clone มา
    public Transform container;
    public float space;
    public float lerpSpeed = 5;

    //เป็นการเรียก class CharObject มาเก็บไว้ใน List น่าจะเป็นพวกเอาคำมาเก็บเป็นtext 
    List<CharObject> charObjects = new List<CharObject>();

    CharObject firstSelected;

    public int currentWord;
    public List<int> numIndexList = new List<int>();

    //อันนี้กำหนดให้เป็นคลาสหลัก
    public static WordSorting main;

    //ใช้ทำเอฟเฟคupdate
    private float totalScore;
    private int wordIndex = 0;
    private int index = 0;
    private bool _init = false;
    private bool _initRAD = false;

    private GameObject Manager;
    private WordManager wordManager;

    void Awake()
    {
        main = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RadWord(0f));
        Manager = GameObject.Find("GameData").gameObject;
        wordManager = Manager.GetComponent<WordManager>();

        while (numIndexList.Count != 20)
        {
            wordIndex = Random.Range(0, 20);
            if (!numIndexList.Contains(wordIndex))
                numIndexList.Add(wordIndex);

        }

        //ทำการปิดหน้า summary ทุกครั้งตอนเริ่มเกม
        result.summaryCanvas.SetActive(false);

        //ShowSorting(wordIndex);
        result.textTotalScore.text = result.totalScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_init && _initRAD && words[index].word != null)
        {
            ShowSorting(index);
            _init = true;
        }
        RepositionObject();

        //ทำเอฟเฟคตอนนับคะแนน
        totalScore = Mathf.Lerp(totalScore, result.totalScore, Time.deltaTime * 5);
        result.textTotalScore.text = Mathf.RoundToInt(totalScore).ToString();
    }

    IEnumerator RadWord(float time)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < 20 - 1; i++)
        {
            words[i].word = wordManager.words[numIndexList[i]].wordname_romanji;
            //words[i].word = WordManager.words[numIndexList[i]].wordname_JP;
        }

        if (!_initRAD)
        {
            _initRAD = true;
        }
    }

    void RepositionObject()
    {
        //เอาคำใน List ที่เก็บจาก CharObject มาแสดงใน Scene โดยใช้ข้างล่างในการแสดง
        //แสดงตรงกลาง อยู่ในกรอบใสๆ
        if (charObjects.Count == 0)
        {
            return;
        }
        float center = (charObjects.Count - 1f) / 2;
        for (int i = 0; i < charObjects.Count; i++)
        {
            charObjects[i].rectTransform.anchoredPosition
                = Vector2.Lerp(charObjects[i].rectTransform.anchoredPosition,
                 new Vector2((i - center) * space, 0), lerpSpeed * Time.deltaTime);
            charObjects[i].index = i;
        }
    }


    /// <summary>
    /// Show a random word to the screen
    /// </summary>
    public void ShowSorting()
    {
        ShowSorting(Random.Range(0, words.Count - 1));
    }

    /// <summary>
    /// Show word from collection with desired index
    /// </summary>
    /// <param name="index">index of the element</param>
    public void ShowSorting(int index)
    {
        //ถ้า index มากกว่าคำที่มีทั้งหมดใน array
        //show หน้า summary
        if (index > words.Count - 1)
        {
            //ถ้าคำหมดแล้วให้แสดงหน้า summaryScoreเลย
            result.ShowSummary();

            //ปิดหน้าเกมด้วย
            wordCanvas.SetActive(false);

            return;
        }

        if (currentWord <= numIndexList.Count)
        {
            result.textJapan.text = wordManager.words[numIndexList[currentWord]].wordname_JP.ToString();
            //result.textJapan.text = WordManager.words[numIndexList[currentWord]].wordname_romanji.ToString();
            result.textMean.text = wordManager.words[numIndexList[currentWord]].word_meaning.ToString();
        }
        //แสดงตามindex 
        charObjects.Clear();
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        //เก็บตัวอักษรที่เอาไป sort ใน function getString 
        char[] chars = words[index].GetString().ToCharArray();
        foreach (char c in chars)
        {
            //Clone Button ออกมาตามจำนวนอักษรที่มีในคำ
            CharObject clone = Instantiate(prefab.gameObject).GetComponent<CharObject>();

            //เป็นการเอาbutton ที่clone มาไปเก็บอยู่ใน container
            clone.transform.SetParent(container);
            clone.transform.localScale = new Vector3(1, 1, 1);

            charObjects.Add(clone.Init(c));
        }

        //ทุกครั้งที่มีคำใหม่ก็นับเวลาเท่าเดิมที่ 15 วิ
        StartCoroutine(TimeLimit());
    }

    public void Swap(int indexA, int indexB)
    {
        CharObject tmpA = charObjects[indexA];

        charObjects[indexA] = charObjects[indexB];
        charObjects[indexB] = tmpA;

        charObjects[indexA].transform.SetAsLastSibling();
        charObjects[indexB].transform.SetAsLastSibling();

        CheckWord();
    }

    //ทำการเลือกปุ่ม
    public void Select(CharObject charObject)
    {
        if (firstSelected)
        {
            //แล้วเอามาสลับ
            Swap(firstSelected.index, charObject.index);

            firstSelected.Select();
            charObject.Select();

        }
        else
        {
            firstSelected = charObject;
        }
    }

    public void UnSelect()
    {
        firstSelected = null;
    }

    /// <summary>
    /// method Checkword เป็นการเช็คคำใน list charObjects
    /// </summary>
    public void CheckWord()
    {
        StartCoroutine(CoCheckWord());
    }

    /// <summary>
    /// เป็นการตั้งเวลาตอนที่สลับคำเสร็จแล้วให้ชะลอเวลาพักนึงเพื่อแสดงว่าคำถูกต้อง
    /// </summary>
    /// <returns></returns>
    IEnumerator CoCheckWord()
    {
        yield return new WaitForSeconds(0.5f);
        string word = "";
        foreach (CharObject charObject in charObjects)
        {
            word += charObject.character;
        }

        //ถ้าเกิดว่าเวลาในคำปัจจุบันหมดลงให้แสดงหน้า summary เลย
        if (timeLimit <= 0)
        {
            result.ShowSummary();
            wordCanvas.SetActive(false);
        }

        //ถ้าคำปัจจุบันเรียงถูกต้องแล้วให้แสดงคำต่อไปสำหรับให้ผู้เล่นเรียงคำให้ถูกต้อง
        if (word.Equals(words[currentWord].word))
        {
            currentWord++;

            //ถ้าถูกให้นับคำเพิ่มไป
            countWords++;

            //เอาคะแนนมาใส่ใน text ได้เลย เก็บคะแนนในแต่ละคำ
            result.totalScore += Mathf.RoundToInt(timeLimit);
            result.textTotalScore.text = result.totalScore.ToString();

            ShowSorting(currentWord);

        }
    }

    float timeLimit;
    IEnumerator TimeLimit()
    {
        timeLimit = 15.0f;
        //อันนี้เขียนสำหรับกำหนดค่าในแต่ละคำ

        //ทุกครั้งที่เรียก Timelimit จะแสดงเวลา15วิได้เร็วกว่ารอมันเข้า while 
        result.textTime.text = Mathf.RoundToInt(timeLimit).ToString();

        int myWord = currentWord;

        yield return new WaitForSeconds(1f);

        while (timeLimit > 0)
        {
            //break ไม่ให้เวลามันวนloop แล้ว timelimit มันทวีคูณความเร็วในการนับเวลา
            if (myWord != currentWord) { yield break; }

            //นับเวลาถอยหลัง
            timeLimit -= Time.deltaTime;
            result.textTime.text = Mathf.RoundToInt(timeLimit).ToString();
            yield return null;
        }

        //ตรงนี้คือเป็นการเช็คเวลา ถ้าเวลาหมดให้ไปเรียก ผ่าน check word ไปยัง cocheckword ถ้าเวลาหมดให้ข้าม
        result.textTotalScore.text = result.totalScore.ToString();
        CheckWord();

    }
    
    public void PauseButton()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseTab.SetActive(true);
            result.scorePause.text = result.totalScore.ToString();
        }
        else
        {
            Time.timeScale = 1;
            pauseTab.SetActive(false);
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
                if (Manager.GetComponent<UserManager>().user.Score2 < totalScore)
                {
                    Manager.GetComponent<UserManager>().user.Score2 = (int)totalScore;
                }

                Manager.GetComponent<UserManager>().Save();
                SceneManager.LoadScene("MainMenu");
                break;
            case (1):
                if (Manager.GetComponent<UserManager>().user.Score2 < totalScore)
                {
                    Manager.GetComponent<UserManager>().user.Score2 = (int)totalScore;
                }

                Manager.GetComponent<UserManager>().Save();
                SceneManager.LoadScene("SortingWord");
                break;
        }
    }
}
