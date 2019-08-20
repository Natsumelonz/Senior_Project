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
                SceneManager.LoadScene("Dialog");
                break;
        }
    }
}
