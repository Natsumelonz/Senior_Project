using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HighScores : MonoBehaviour
{
    const string privateCode1 = "AWL9-te4h0-ZD3hSFBCoqwSDXksa_fHk-JxsChTqZlIg";
    const string publicCode1 = "5d7c6d49d1041303ec9761ce";
    const string privateCode2 = "NAYr6U-4gUG3bVccX5p2-QJHPL-oES1USNT6ww7orCqA";
    const string publicCode2 = "5d85f710d1041303ecc68ea5";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoresList1;
    public Highscore[] highscoresList2;
    DisplayHighScores highscoresDisplay;
    public static HighScores Instance { set; get; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        highscoresDisplay = GetComponent<DisplayHighScores>();
    }

    public static void AddNewHighscore1(string username, int score)
    {
        Instance.StartCoroutine(Instance.UploadNewHighscore1(username, score));
    }
    public static void AddNewHighscore2(string username, int score)
    {
        Instance.StartCoroutine(Instance.UploadNewHighscore2(username, score));
    }

    IEnumerator UploadNewHighscore1(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode1 + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
        }
        else
        {
            print("Error uploading: " + www.error);
        }
    }

    IEnumerator UploadNewHighscore2(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode2 + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
        }
        else
        {
            print("Error uploading: " + www.error);
        }
    }

    public void DownloadHighscores()
    {
        StartCoroutine(DownloadHighscoresFromDataBase1());
        StartCoroutine(DownloadHighscoresFromDataBase2());
    }

    IEnumerator DownloadHighscoresFromDataBase1()
    {
        WWW www1 = new WWW(webURL + publicCode1 + "/pipe/");
        yield return www1;
        if (string.IsNullOrEmpty(www1.error))
        {
            FormatHighscores1(www1.text);
            highscoresDisplay.OnHighscoresDownloaded1(highscoresList1);
        }
        else
        {
            print("Error Downloading: " + www1.error);
        }
    }

    IEnumerator DownloadHighscoresFromDataBase2()
    {


        WWW www2 = new WWW(webURL + publicCode2 + "/pipe/");
        yield return www2;
        if (string.IsNullOrEmpty(www2.error))
        {
            FormatHighscores2(www2.text);
            highscoresDisplay.OnHighscoresDownloaded2(highscoresList2);
        }
        else
        {
            print("Error Downloading: " + www2.error);
        }
    }

    void FormatHighscores1(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList1 = new Highscore[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList1[i] = new Highscore(username, score);

            print(highscoresList1[i].username + ": " + highscoresList1[i].score);
        }
        print(highscoresList1.Length);
    }
    void FormatHighscores2(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList2 = new Highscore[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList2[i] = new Highscore(username, score);

            print(highscoresList2[i].username + ": " + highscoresList2[i].score);
        }
        print(highscoresList2.Length);
    }
}

public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}

