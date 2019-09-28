using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharObjectOfChapter : MonoBehaviour
{
    public string sentence;
    public Text text;
    public Image image;
    public RectTransform rectTransform;
    public int index;

    [Header("Appearance")]
    public Color normalColor;
    public Color selectedColor;


    public CharObjectOfChapter Init(string c)
    {
        sentence = c;
        text.text = c.ToString();
        gameObject.SetActive(true);
        return this;
    }

    public void Select()
    {
        ChapterScene.main.CheckAnswer(this);
    }
}