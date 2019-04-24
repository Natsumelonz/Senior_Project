using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RetreiveWord
{
    public int word_id;
    public string wordname_JP;
    public string wordname_romanji;
    public string word_meaning;
    public int word_syllable;

    public override string ToString()
    {
        return JsonUtility.ToJson(this,true);
    }
}
