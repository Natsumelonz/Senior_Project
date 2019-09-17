using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Proyecto26;

public class MenuBehaviour : MonoBehaviour
{
    public AlpabetManager alpabetManagement = new AlpabetManager();
    public WordManager wordManager = new WordManager();
    public DialogManager dialogManager = new DialogManager();
    public QuestionManager questionManager = new QuestionManager();
    public static bool init = false;

    void OnDisable()
    {
        PlayerPrefs.SetInt("level", 1);
    }

    private void Start()
    {
        if (init == false)
        {
            alpabetManagement.PullAlphabets();
            wordManager.PullWords();
            dialogManager.PullDialogCH1();
            dialogManager.PullDialogCH2();
            questionManager.PullQuestionCH1();
            questionManager.PullQuestionCH2();

            init = true;
        }
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
                SceneManager.LoadScene("Leaderboard");
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
