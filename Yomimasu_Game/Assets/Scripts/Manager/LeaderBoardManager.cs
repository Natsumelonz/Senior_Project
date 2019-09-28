using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Proyecto26;
using System.Linq;

[Serializable]
public class LeaderBoard
{
    public string Name;
    public int Score;

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}

[Serializable]
public class LeaderBoardManager : MonoBehaviour
{
    public static LeaderBoardManager Instance { set; get; }
    public List<LeaderBoard> leaderBoard1 = new List<LeaderBoard>();
    public List<LeaderBoard> leaderBoard2 = new List<LeaderBoard>();
    public LeaderBoard userScore1;
    public LeaderBoard userScore2;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        PullLeaderBoard1();
        PullLeaderBoard2();
    }

    public void PullLeaderBoard1()
    {
        leaderBoard1.Clear();
        RestClient.GetArray<LeaderBoard>("https://it59-28yomimasu.firebaseio.com/Score/Matching.json").Then(response =>
        {
            for (int i = 0; i <= response.Length; i++)
            {
                leaderBoard1.Add(response[i]);
                //Debug.Log(leaderBoard1[i].Name);
                //Debug.Log(leaderBoard1[i].Score);
            }
        });

        Debug.Log("Initial LeaderBoard1 Complete!");
    }

    public void PullLeaderBoard2()
    {
        leaderBoard2.Clear();
        RestClient.GetArray<LeaderBoard>("https://it59-28yomimasu.firebaseio.com/Score/Scramble.json").Then(response =>
        {
            for (int i = 0; i <= response.Length; i++)
            {
                leaderBoard2.Add(response[i]);
                //Debug.Log(leaderBoard1[i].Name);
                //Debug.Log(leaderBoard1[i].Score);
            }
        });

        Debug.Log("Initial LeaderBoard2 Complete!");
    }

    public void PutLeaderBoard1()
    {
        int index1 = leaderBoard1.FindIndex(item => item.Name == userScore1.Name);
        int index2 = leaderBoard1.FindIndex(item => item.Score < userScore1.Score && item.Name == userScore1.Name);

        if (index1 >= 0)
        {
            leaderBoard1[index1] = userScore1;
        }
        else if (!leaderBoard1.Contains(userScore1))
        {
            leaderBoard1.Add(userScore1);
        }

        leaderBoard1.Sort(delegate (LeaderBoard l1, LeaderBoard l2)
        {
            return l2.Score.CompareTo(l1.Score);
        });

        if (leaderBoard1.Count > 10)
        {
            leaderBoard1.RemoveAt(10);
        }

        for (int i = 0; i < leaderBoard1.Count; i++)
        {
            RestClient.Put("https://it59-28yomimasu.firebaseio.com/Score/Matching/" + i + ".json", leaderBoard1[i]);
        }
    }

    public void PutLeaderBoard2()
    {
        int index1 = leaderBoard2.FindIndex(item => item.Name == userScore2.Name);
        int index2 = leaderBoard2.FindIndex(item => item.Score < userScore2.Score && item.Name == userScore2.Name);

        if (index1 >= 0)
        {
            leaderBoard2[index1] = userScore2;
        }
        else if (!leaderBoard2.Contains(userScore2))
        {
            leaderBoard2.Add(userScore2);
        }

        leaderBoard2.Sort(delegate (LeaderBoard l1, LeaderBoard l2)
        {
            return l2.Score.CompareTo(l1.Score);
        });

        if (leaderBoard2.Count > 10)
        {
            leaderBoard2.RemoveAt(10);
        }

        for (int i = 0; i < leaderBoard2.Count; i++)
        {
            RestClient.Put("https://it59-28yomimasu.firebaseio.com/Score/Scramble/" + i + ".json", leaderBoard2[i]);
        }
    }
}
