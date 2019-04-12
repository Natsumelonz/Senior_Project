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
                SceneManager.LoadScene("GameMatching");
                break;
            case (1):
                Application.Quit();
                break;
        }
    }
}
