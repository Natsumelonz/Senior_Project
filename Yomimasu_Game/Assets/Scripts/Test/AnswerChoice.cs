using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerChoice : MonoBehaviour
{
    public string answer;
    public Text text;
    public int index;
    public AnswerChoice Init(string c, int i)
    {
        answer = c;
        text.text = c.ToString();
        index = i;
        return this;
    }

    public void CheckAnswer()
    {
        TestScene.main.CheckAnswer(this);
    }
}
