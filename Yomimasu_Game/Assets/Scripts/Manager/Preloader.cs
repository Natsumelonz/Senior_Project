using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    public AlpabetManager alpabetManagement = new AlpabetManager();
    public WordManager wordManager = new WordManager();
    public DialogManager dialogManager = new DialogManager();
    public QuestionManager questionManager = new QuestionManager();
    public static bool init = false;
    public User user = new User();

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (init == false)
        {
            alpabetManagement.PullAlphabets();
            wordManager.PullWords();
            dialogManager.PullDialogCH1();
            dialogManager.PullDialogCH2();
            questionManager.PullQuestionCH1();

            init = true;
        }
    }
}
