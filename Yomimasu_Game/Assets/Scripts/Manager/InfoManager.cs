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
    private RectTransform graphContainer3;
    private RectTransform graphContainer4;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    private int panelPage = 0;

    public List<int> userPost;
    public List<int> userPre;
    public Text userName;
    public Text lastCh;
    public Text score1;
    public Text score2;
    public GameObject gC1;
    public GameObject gC2;
    public GameObject gC3;
    public GameObject gC1Panel;
    public GameObject gC2Panel;
    public GameObject gC3Panel;
    public GameObject infoPanel;
    public GameObject statsPanel;
    public GameObject graphPanel;
    public Text high1;
    public Text high2;
    public Text gameName;
    public Sprite circleSprite;
    public Button arrowLeft;
    public Button arrowRight;

    private void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
        Audio = GameObject.Find("AudioManager").gameObject;
        Effect = GameObject.Find("EffectManager").gameObject;

        userName.text = Manager.GetComponent<UserManager>().user.Name;
        lastCh.text = Manager.GetComponent<UserManager>().user.LastCh.ToString();
        score1.text = Manager.GetComponent<UserManager>().user.Score1 + " pt.";
        score2.text = Manager.GetComponent<UserManager>().user.Score2 + " pt.";
        
        graphContainer1 = gC1.GetComponent<RectTransform>();
        graphContainer2 = gC2.GetComponent<RectTransform>();
        graphContainer3 = gC3.GetComponent<RectTransform>();
        graphContainer4 = gC3.GetComponent<RectTransform>();

        ShowGraph(Manager.GetComponent<UserManager>().user.LastScore1, Manager.GetComponent<UserManager>().user.Score1, graphContainer1, 9.5f, false);
        high1.text = Manager.GetComponent<UserManager>().user.Score1.ToString();

        ShowGraph(Manager.GetComponent<UserManager>().user.LastScore2, Manager.GetComponent<UserManager>().user.Score2, graphContainer2, 9.5f, false);
        high2.text = Manager.GetComponent<UserManager>().user.Score2.ToString();

        for (int i = 0; i < Manager.GetComponent<UserManager>().user.Post.Length; i++)
        {
            userPost.Add(Manager.GetComponent<UserManager>().user.Post[i]);
            userPre.Add(Manager.GetComponent<UserManager>().user.Pre[i]);
            //Debug.Log(userPre[i]);
        }

        ShowGraph(userPost, 10f, graphContainer3, 7.5f, true);
        ShowGraph(userPre, 10f, graphContainer4, 7.5f, false);
        high2.text = Manager.GetComponent<UserManager>().user.Score2.ToString();
               
        gameName.text = "Matching Game";

        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
    }

    public void Update()
    {
        Reposition();
        if (panelPage <= 0)
        {
            arrowLeft.interactable = false;
        }
        else
        {
            arrowLeft.interactable = true;
        }
        if (panelPage >= 2)
        {
            arrowRight.interactable = false;
        }
        else
        {
            arrowRight.interactable = true;
        }

        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            if (arrowLeft.interactable)
                            {
                                SwitchPanel(1);
                            }
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            if (arrowRight.interactable)
                            {
                                SwitchPanel(-1);
                            }
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }

    public GameObject CreateCircle(Vector2 anchorP, RectTransform graphContainer, bool post)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        if (post)
        {
            gameObject.GetComponent<Image>().color = new Color32(91, 219, 0, 255);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(252, 255, 0, 255);
        }
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchorP;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    public void CreateDotConnection(Vector2 dotA, Vector2 dotB, RectTransform graphContainer, bool post)
    {
        GameObject game = new GameObject("dotConnection", typeof(Image));
        game.transform.SetParent(graphContainer, false);
        if (post)
        {
            game.GetComponent<Image>().color = new Color32(91, 219, 0, 255);
        }
        else
        {
            game.GetComponent<Image>().color = new Color(252, 255, 0, 255);
        }
        RectTransform transform = game.GetComponent<RectTransform>();
        Vector2 dir = (dotB - dotA).normalized;
        float dis = Vector2.Distance(dotA, dotB);
        transform.anchorMin = new Vector2(0, 0);
        transform.anchorMax = new Vector2(0, 0);
        transform.sizeDelta = new Vector2(dis, 3f);
        transform.anchoredPosition = dotA + dir * dis * .5f;
        transform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
    }

    public void ShowGraph(List<int> valueList, float yMaximum, RectTransform graphContainer, float x, bool post)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWeight = graphContainer.sizeDelta.x;

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = 20f + i * (graphWeight / x);
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), graphContainer, post);
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                    circleGameObject.GetComponent<RectTransform>().anchoredPosition, graphContainer, post);
            }
            lastCircleGameObject = circleGameObject;
        }
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

    public void PrePostStats()
    {
        if (infoPanel.activeSelf)
        {
            infoPanel.SetActive(false);
            statsPanel.SetActive(true);
        }
        else
        {
            statsPanel.SetActive(false);
            infoPanel.SetActive(true);
        }
    }

    public void SwitchPanel(int i)
    {
        if (panelPage > -1 && panelPage < 3)
        {
            panelPage -= i;
        }

        //Debug.Log(panelPage);
    }

    public void Reposition()
    {
        RectTransform transform1 = infoPanel.GetComponent<RectTransform>();
        RectTransform transform2 = graphPanel.GetComponent<RectTransform>();
        RectTransform transform3 = statsPanel.GetComponent<RectTransform>();

        switch (panelPage)
        {
            default:
                break;
            case (0):
                transform1.anchoredPosition = Vector2.Lerp(transform1.anchoredPosition, new Vector2(0, 0), 5 * Time.deltaTime);
                transform2.anchoredPosition = Vector2.Lerp(transform2.anchoredPosition, new Vector2(1100f, 0), 5 * Time.deltaTime);
                transform3.anchoredPosition = Vector2.Lerp(transform3.anchoredPosition, new Vector2(2100f, 0), 5 * Time.deltaTime);
                break;
            case (1):
                transform1.anchoredPosition = Vector2.Lerp(transform1.anchoredPosition, new Vector2(-1100f, 0), 5 * Time.deltaTime);
                transform2.anchoredPosition = Vector2.Lerp(transform2.anchoredPosition, new Vector2(0, 0), 5 * Time.deltaTime);
                transform3.anchoredPosition = Vector2.Lerp(transform3.anchoredPosition, new Vector2(1100f, 0), 5 * Time.deltaTime);
                break;
            case (2):
                transform1.anchoredPosition = Vector2.Lerp(transform1.anchoredPosition, new Vector2(-2100f, 0), 5 * Time.deltaTime);
                transform2.anchoredPosition = Vector2.Lerp(transform2.anchoredPosition, new Vector2(-1100f, 0), 5 * Time.deltaTime);
                transform3.anchoredPosition = Vector2.Lerp(transform3.anchoredPosition, new Vector2(0f, 0), 5 * Time.deltaTime);
                break;
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
