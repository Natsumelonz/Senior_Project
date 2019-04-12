using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{   
    public void triggerMenuBehavior(int i)
    {
        switch (i)
        {
            default:
            case (0):
                SceneManager.LoadScene("Level");
                break;
            case (1):
                Application.Quit(1);
                break;
        }
    }
    
}
