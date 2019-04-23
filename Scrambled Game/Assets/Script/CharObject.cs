using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharObject : MonoBehaviour
{
    public char character;
    //ใช้รับText
    public Text text;
    public Image image;
    //ใช้ใส่ rect ของปุ่ม ตอนสลับปุ่ม
    public RectTransform rectTransform;
    public int index;

    [Header("Appearance")]
    public Color normalColor;
    public Color selectedColor;

    bool isSelected = false;

    public CharObject Init (char c)
    {
        character = c;
        text.text = c.ToString();
        gameObject.SetActive(true);
        return this;
    }
    //แสดงสีที่ปุ่มเวลากดที่ปุ่ม เป็นสีเหลือง
    public void Select()
    {
        isSelected = !isSelected;
        image.color = isSelected ? selectedColor : normalColor;
        if (isSelected)
        {
            WordSorting.main.Select(this);
        }
        else
        {
            WordSorting.main.UnSelect();
        }
    }
    
}
