using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MatchingManager : MonoBehaviour
{
    public Sprite carded;
    public Sprite card;
    public GameObject[] cards;
    public GameObject[] cards2;
    public GameObject[] cards1;
    public Text point;

    private bool _init = false;
    private int _point = 0;

    // Update is called once per frame
    void Update()
    {
        if (!_init)
        {
            initializeCard();
        }

        if (Input.GetMouseButtonUp(0))
        {
            checkCards();
        }
    }


    //สุ่มค่าให้ปุ่ม กับกำหนด Text
    void initializeCard()
    {
        //for (int id = 0; id < 2; id++)
        //{

        //สุ่มปุ่มฝั่งซ้าย
        for (int i = 0; i < 12; i++)
        {
            bool test = false;
            int choice = 0;
            while (!test)
            {
                choice = Random.Range(0, cards.Length);
                test = !(cards[choice].GetComponent<Card>().initialized);
            }
            cards[choice].GetComponentInChildren<Text>().text = i.ToString();
            cards[choice].GetComponent<Card>().cardValue = i;
            cards[choice].GetComponent<Card>().initialized = true;

        }

        //สุ่มปุ่มฝั่งขวา
        for (int i = 0; i < 12; i++)
        {
            bool test = false;
            int choice = 0;
            while (!test)
            {
                choice = Random.Range(0, cards1.Length);
                test = !(cards1[choice].GetComponent<Card>().initialized);
            }
            cards1[choice].GetComponentInChildren<Text>().text = i.ToString();
            cards1[choice].GetComponent<Card>().cardValue = i;
            cards1[choice].GetComponent<Card>().initialized = true;

        }

        //}

        //เรียก Method ให้กดหนด Sprite ให้ปุ่ม ฝั่งซ้าย
        foreach (GameObject c in cards)
        {
            c.GetComponent<Card>().setupGraphics();
        }

        //เรียก Method ให้กดหนด Sprite ให้ปุ่ม ฝั่งขวา
        foreach (GameObject c in cards1)
        {
            c.GetComponent<Card>().setupGraphics();
        }

        //ทำการบอกว่าได้สุ่มค่าให้ปุ่มแล้ว
        if (!_init)
        {
            _init = true;
        }

    }

    //Method เรียก Sprite แบบยังไม่ได้กด
    public Sprite getCard()
    {
        return card;
    }

    //Method เรียก Sprite แบบกดแล้ว
    public Sprite getCarded()
    {
        return carded;
    }

    //Method เอาไว้ตรวจว่ากดปุ่มทั้ง 2 ปุ่มแล้วนำไปเทียบค่ากัน
    void checkCards()
    {
        List<int> t = new List<int>();

        for (int i = 0; i < cards2.Length; i++)
        {
            if (cards2[i].GetComponent<Card>().state == 1)
            {
                t.Add(i);
            }
        }

        if (t.Count == 2)
        {
            cardComparison(t);
        }
    }

    //Method เทียบค่าของปุ่มที่กดทั้ง 2 ปุ่ม
    void cardComparison(List<int> t)
    {
        Card.DO_NOT = true;

        int x = 0;
        if (cards2[t[0]].GetComponent<Card>().cardValue == cards2[t[1]].GetComponent<Card>().cardValue)
        {
            x = 2;
            _point++;
            point.text = "Point : " + _point;
            if (_point == 12)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        for (int i = 0; i < t.Count; i++)
        {
            cards2[t[i]].GetComponent<Card>().state = x;
            cards2[t[i]].GetComponent<Card>().falseCheck();
        }
    }
}
