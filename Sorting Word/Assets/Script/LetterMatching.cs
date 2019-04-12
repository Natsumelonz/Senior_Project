using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterMatching : MonoBehaviour
{
    public static bool DO_NOT = false;
    [SerializeField]
    private int _buttonValue;
    [SerializeField]
    private bool _initialized = false;

    private Sprite buttonSelect;
    //private Sprite buttonUnselect;

    private GameObject manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        
    }
    public void setupButton()
    {
        buttonSelect = manager.GetComponent<GameManager>().getButtonSelect(_buttonValue);
        //buttonUnselect = manager.GetComponent<GameManager>().getButtonUnselect();

        
    }
    

    public int buttonValue
    {
        get { return _buttonValue; }
        set { _buttonValue = value; }
    }
    public bool initilized
    {
        get { return _initialized; }
        set { _initialized = value; }
    }
    public void falseCheck()
    {
        StartCoroutine(pause());
    }
    IEnumerator pause()
    {
        yield return new WaitForSeconds(1);
        
    }
}
