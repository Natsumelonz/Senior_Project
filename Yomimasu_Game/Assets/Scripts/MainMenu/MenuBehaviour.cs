using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Proyecto26;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
    private GameObject Manager;
    public GameObject Panel;
    public Text playerName;

    void OnDisable()
    {
        PlayerPrefs.SetInt("level", 1);
    }

    private void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        playerName.text = Manager.GetComponent<UserManager>().user.Name;
    }

    public void ClosePanel()
    {
        Panel.SetActive(false);
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
                if (Manager.GetComponent<UserManager>().user.LastCh < 4)
                {
                    Panel.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("AnotherChapter");
                }
                break;
            case (5):
                SceneManager.LoadScene("Chapter_1");
                break;
            case (6):
                if (Manager.GetComponent<UserManager>().user.LastCh < 1)
                {
                    Panel.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("Chapter_2");
                }
                break;
            case (7):
                if (Manager.GetComponent<UserManager>().user.LastCh < 2)
                {
                    Panel.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("Chapter_3");
                }
                break;
            case (8):
                if (Manager.GetComponent<UserManager>().user.LastCh < 3)
                {
                    Panel.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("Chapter_4");
                }
                break;
            case (9):
                if (Manager.GetComponent<UserManager>().user.LastCh < 4)
                {
                    Panel.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("Chapter_5");
                }
                break;
            case (10):
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
