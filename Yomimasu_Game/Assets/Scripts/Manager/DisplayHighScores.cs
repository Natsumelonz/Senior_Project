using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScores : MonoBehaviour
{
    public Text[] highscoreText1;
    public Text[] highscoreText2;
    public GameObject scoreM;
    public GameObject scoreS;
    HighScores highscoreManager;
    private GameObject Manager;

    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        for (int i = 0; i < highscoreText1.Length; i++)
        {
            highscoreText1[i].text = i + 1 + ". Fetching...";
        }
        for (int i = 0; i < highscoreText2.Length; i++)
        {
            highscoreText2[i].text = i + 1 + ". Fetching...";
        }

        highscoreManager = GetComponent<HighScores>();
        highscoreManager.DownloadHighscores();  
        StartCoroutine(RefreshHighscores());
    }
    public void OnHighscoresDownloaded1(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreText1.Length; i++)
        {
            highscoreText1[i].text = i + 1 + ".  ";
            if (highscoreList.Length > 0)
            {
                highscoreText1[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
            }
        }
    }

    public void OnHighscoresDownloaded2(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreText2.Length; i++)
        {
            highscoreText2[i].text = i + 1 + ".  ";
            if (highscoreList.Length > 0)
            {
                highscoreText2[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
            }
        }
    }

    public void SelectScore(int i)
    {
        switch (i)
        {
            default:
                break;
            case (1):
                scoreM.SetActive(false);
                scoreS.SetActive(true);
                break;
            case (2):
                scoreM.SetActive(true);
                scoreS.SetActive(false);
                break;
        }
    }

    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
            highscoreManager.DownloadHighscores();
        }
    }
}
