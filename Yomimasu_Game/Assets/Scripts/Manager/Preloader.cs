using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    private GameObject Manager;

    void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;
    }

    void Update()
    {
        if (Manager.GetComponent<AlpabetManager>().alphabetsHR.Count != 0 && Manager.GetComponent<AlpabetManager>().alphabetsKT.Count != 0 
            && Manager.GetComponent<WordManager>().words.Count != 0 && Manager.GetComponent<LeaderBoardManager>().leaderBoard1.Count != 0 
            && Manager.GetComponent<LeaderBoardManager>().leaderBoard2.Count != 0 && Manager.GetComponent<UserManager>().user.Name != ""
            && Manager.GetComponent<ContentManager>().chapter1_info.chap_id != 0 && Manager.GetComponent<ContentManager>().chapter2_info.chap_id != 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
