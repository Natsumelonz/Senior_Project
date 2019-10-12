using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Proyecto26;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoManager : MonoBehaviour
{
    private GameObject Manager;
    private GameObject Audio;
    private GameObject Effect;
    private RectTransform graphContainer1;
    private RectTransform graphContainer2;

    public Text userName;
    public Text lastCh;
    public Text score1;
    public Text score2;
    public List<Text> preScore;
    public List<Text> postScore;
    public GameObject gC1;
    public GameObject gC2;
    public GameObject gC1Panel;
    public GameObject gC2Panel;
    public Text high1;
    public Text high2;
    public Text gameName;
    public Sprite circleSprite;

    private void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        Audio = GameObject.Find("AudioManager").gameObject;
        Effect = GameObject.Find("EffectManager").gameObject;

        userName.text = Manager.GetComponent<UserManager>().user.Name;
        lastCh.text = Manager.GetComponent<UserManager>().user.LastCh.ToString();
        score1.text = Manager.GetComponent<UserManager>().user.Score1 + " pt.";
        score2.text = Manager.GetComponent<UserManager>().user.Score2 + " pt.";

        for (int i = 0; i < Manager.GetComponent<UserManager>().user.Pre.Length; i++)
        {
            preScore[i].text = "Ch." + (i + 1) + ": " + Manager.GetComponent<UserManager>().user.Pre[i] + " pt.";
        }

        for (int i = 0; i < Manager.GetComponent<UserManager>().user.Post.Length; i++)
        {
            postScore[i].text = "Ch." + (i + 1) + ": " + Manager.GetComponent<UserManager>().user.Post[i] + " pt.";
        }

        graphContainer1 = gC1.GetComponent<RectTransform>();
        graphContainer2 = gC2.GetComponent<RectTransform>();

        ShowGraph(Manager.GetComponent<UserManager>().user.LastScore1, Manager.GetComponent<UserManager>().user.Score1, graphContainer1);
        high1.text = Manager.GetComponent<UserManager>().user.Score1.ToString();

        ShowGraph(Manager.GetComponent<UserManager>().user.LastScore2, Manager.GetComponent<UserManager>().user.Score2, graphContainer2);
        high2.text = Manager.GetComponent<UserManager>().user.Score2.ToString();
        gameName.text = "Matching Game";

    }

    public GameObject CreateCircle(Vector2 anchorP, RectTransform graphContainer)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchorP;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    public void ShowGraph(List<int> valueList, float yMaximum, RectTransform graphContainer)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWeight = graphContainer.sizeDelta.x;

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = 20f + i * (graphWeight / 9.5f);
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), graphContainer);
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, graphContainer);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    public void CreateDotConnection(Vector2 dotA, Vector2 dotB, RectTransform graphContainer)
    {
        GameObject game = new GameObject("dotConnection", typeof(Image));
        game.transform.SetParent(graphContainer, false);
        game.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform transform = game.GetComponent<RectTransform>();
        Vector2 dir = (dotB - dotA).normalized;
        float dis = Vector2.Distance(dotA, dotB);
        transform.anchorMin = new Vector2(0, 0);
        transform.anchorMax = new Vector2(0, 0);
        transform.sizeDelta = new Vector2(dis, 3f);
        transform.anchoredPosition = dotA + dir * dis * .5f;
        transform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
    }
    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    public void Switch()
    {
        if (gC1Panel.activeSelf)
        {
            gC1Panel.SetActive(false);
            gC2Panel.SetActive(true);
            gameName.text = "Scramble Game";
        }
        else
        {
            gC2Panel.SetActive(false);
            gC1Panel.SetActive(true);
            gameName.text = "Matching Game";
        }
    }

    public void MainMenu()
    {
        Effect.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetInfo()
    {
        Effect.GetComponent<AudioSource>().Play();
        Manager.GetComponent<UserManager>().user.LastCh = 0;
        Manager.GetComponent<UserManager>().user.Score1 = 0;
        Manager.GetComponent<UserManager>().user.Score2 = 0;
        Manager.GetComponent<UserManager>().user.Pre = new int[10];
        Manager.GetComponent<UserManager>().user.Post = new int[10];
        Manager.GetComponent<UserManager>().user.PassPre = new bool[10];
        Manager.GetComponent<UserManager>().user.PassPost = new bool[10];
        Manager.GetComponent<UserManager>().user.LastScore1 = new List<int>();
        Manager.GetComponent<UserManager>().user.LastScore2 = new List<int>();
        Manager.GetComponent<UserManager>().SaveUser();
        Manager.GetComponent<UserManager>().LoadUser();

        SceneManager.LoadScene("MainMenu");
    }
}
