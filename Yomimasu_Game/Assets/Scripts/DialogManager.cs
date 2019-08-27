using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Proyecto26;

[Serializable]
public class RetrieveDialog
{
    public string script_id;
    public string script_role;
    public string script_desc;
    public bool script_event;

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}

public class DialogManager
{
    public static List<RetrieveDialog> dialog = new List<RetrieveDialog>();
    public void PullDialog()
    {
        RestClient.GetArray<RetrieveDialog>("https://it59-28yomimasu.firebaseio.com/Content/Chapter/Chapter1/Scripts.json").Then(response =>
        {
            for (int i = 0; i <= response.Length; i++)
            {
                dialog.Add(response[i]);
                //Debug.Log(dialog[i].script_id + " | " + dialog[i].script_role + " | " + dialog[i].script_desc + " | " + dialog[i].script_event);
            }
        });

        Debug.Log("Initial Dialog Complete!");
    }
}
