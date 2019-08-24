using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChapterSelect(int i)
    {
        switch (i)
        {
            default:
                break;
            case (0):
                SceneManager.LoadScene("AnotherChapter");
                break;
            case (1):
                SceneManager.LoadScene("Chapter_1");
                break;
            case (2):
                SceneManager.LoadScene("Chapter_2");
                break;
            case (3):
                SceneManager.LoadScene("Chapter_3");
                break;
            case (4):
                SceneManager.LoadScene("Chapter_4");
                break;
            case (5):
                SceneManager.LoadScene("Chapter_5");
                break;
        }
    }
}
