using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Proyecto26;

public class MenuBehaviour : MonoBehaviour
{
    AlpabetManager alpabetManagement = new AlpabetManager();
    WordManager wordManager = new WordManager();
    DialogManager dialogManager = new DialogManager();
    void OnDisable()
    {
        PlayerPrefs.SetInt("level", 1);
    }

    private void Start()
    {
        alpabetManagement.PullAlphabets();
        wordManager.PullWords();
        dialogManager.PullDialog();
    }

    private void Update()
    {

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
            case (3):
                SceneManager.LoadScene("Chapter");
                break;
            case (4):
                SceneManager.LoadScene("AnotherChapter");
                break;
            case (5):
                SceneManager.LoadScene("Chapter_1");
                break;
            case (6):
                SceneManager.LoadScene("Chapter_2");
                break;
            case (7):
                SceneManager.LoadScene("Chapter_3");
                break;
            case (8):
                SceneManager.LoadScene("Chapter_4");
                break;
            case (9):
                SceneManager.LoadScene("Chapter_5");
                break;
        }
    }
}
