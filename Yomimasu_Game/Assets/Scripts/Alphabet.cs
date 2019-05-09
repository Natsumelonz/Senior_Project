using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Alphabet
{
    public string alphabet_id;
    public string alphabetname_JP;
    public string alphabetname_romanji;

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }   
    

}
