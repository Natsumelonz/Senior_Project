using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLeaderBoard : MonoBehaviour
{
    public Text[] leaderBoardNameText1;
    public Text[] leaderBoardScoreText1;
    public Text[] leaderBoardNameText2;
    public Text[] leaderBoardScoreText2;
    public GameObject scoreM;
    public GameObject scoreC;
    private GameObject Manager;
    private GameObject Effect;
    private List<LeaderBoard> l1 = new List<LeaderBoard>();
    private List<LeaderBoard> l2 = new List<LeaderBoard>();

    private void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        Effect = GameObject.Find("EffectManager").gameObject;

        l1 = Manager.GetComponent<LeaderBoardManager>().leaderBoard1;
        l2 = Manager.GetComponent<LeaderBoardManager>().leaderBoard2;

        for (int i = 0; i < leaderBoardNameText1.Length; i++)
        {
            leaderBoardNameText1[i].text = i + 1 + ". Fetching...";
            leaderBoardScoreText1[i].text = i + 1 + ". Fetching...";
        }
        for (int i = 0; i < leaderBoardNameText1.Length; i++)
        {
            leaderBoardNameText1[i].text = i + 1 + ". Fetching...";
            leaderBoardScoreText1[i].text = i + 1 + ". Fetching...";
        }
    }

    private void Update()
    {
        l1.Sort(delegate (LeaderBoard l1, LeaderBoard l2)
        {
            return l2.Score.CompareTo(l1.Score);
        });

        l2.Sort(delegate (LeaderBoard l1, LeaderBoard l2)
        {
            return l2.Score.CompareTo(l1.Score);
        });

        ShowLeaderBoard();
    }

    public void ShowLeaderBoard()
    {
        for (int i = 0; i < l1.Count && i < 10; i++)
        {
            if (l1.Count > 0)
            {
                leaderBoardNameText1[i].text = l1[i].Name;
                leaderBoardScoreText1[i].text = l1[i].Score.ToString();
            }
        }

        for (int i = 0; i < l2.Count && i < 10; i++)
        {
            if (l2.Count > 0)
            {
                leaderBoardNameText2[i].text = l2[i].Name;
                leaderBoardScoreText2[i].text = l2[i].Score.ToString();
            }
        }
    }

    public void SwitchLeaderBoard(int i)
    {
        Effect.GetComponent<AudioSource>().Play();
        switch (i)
        {
            default:
                break;
            case (0):
                scoreC.SetActive(false);
                scoreM.SetActive(true);
                Manager.GetComponent<LeaderBoardManager>().PullLeaderBoard1();
                break;
            case (1):
                scoreM.SetActive(false);
                scoreC.SetActive(true);
                Manager.GetComponent<LeaderBoardManager>().PullLeaderBoard2();
                break;
        }
    }
}
