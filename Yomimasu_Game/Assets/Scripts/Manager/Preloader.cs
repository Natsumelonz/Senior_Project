using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Preloader : MonoBehaviour
{
    private GameObject Manager;
    private AsyncOperation async;

    public GameObject loading;
    public Slider slider;

    void Start()
    {
        Manager = GameObject.Find("GameData").gameObject;

        StartCoroutine(LoadingScreen());
    }

    public IEnumerator LoadingScreen()
    {
        loading.SetActive(true);
        async = SceneManager.LoadSceneAsync("MainMenu");
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            slider.value = async.progress;
            yield return new WaitForEndOfFrame();

            if (async.progress == 0.9f
            && Manager.GetComponent<AlphabetManager>().alphabetsHR.Count != 0 && Manager.GetComponent<AlphabetManager>().alphabetsKT.Count != 0
            && Manager.GetComponent<WordManager>().words.Count != 0 && Manager.GetComponent<LeaderBoardManager>().leaderBoard1.Count != 0
            && Manager.GetComponent<LeaderBoardManager>().leaderBoard2.Count != 0 && Manager.GetComponent<UserManager>().user.Name != ""
            && Manager.GetComponent<ContentManager>().chapters_info[0].chap_id != 0 && Manager.GetComponent<ContentManager>().chapters_info[1].chap_id != 0
            && Manager.GetComponent<ContentManager>().chapters_info[2].chap_id != 0 && Manager.GetComponent<ContentManager>().chapters_info[3].chap_id != 0
            && Manager.GetComponent<ContentManager>().chapters_info[4].chap_id != 0 && Manager.GetComponent<ContentManager>().chapters_info[5].chap_id != 0
            && Manager.GetComponent<ContentManager>().chapters_info[6].chap_id != 0 && Manager.GetComponent<ContentManager>().chapters_info[7].chap_id != 0
            && !UserManager.fistTime)
            {
                //yield return new WaitForSeconds(1f);
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
        }
    }
}
