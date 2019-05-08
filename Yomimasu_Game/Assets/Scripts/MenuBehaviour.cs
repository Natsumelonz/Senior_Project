using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour
{
    public void TriggerMenuBehaviour (int i)
    {
        switch (i)
        {
            default:
                break;
            case (0):
                Application.Quit();                
                break;
            case (1):
                SceneManager.LoadScene("GameMatching");
                break;
            case (2):
                SceneManager.LoadScene("SortingWord");
                break;
        }
    }
}
