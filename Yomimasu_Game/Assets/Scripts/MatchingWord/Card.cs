using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Card : MonoBehaviour
{
    public static bool DO_NOT = false;

    [SerializeField]
    private int _state;
    [SerializeField]
    private int _cardValue;
    [SerializeField]
    private bool _initialized = false;
    [SerializeField]
    private int _side;

    private Sprite _card;
    private Sprite _carded;
    private Animator anim;
    private int highlightedHash = Animator.StringToHash("Highlighted");
    private int disabledHash = Animator.StringToHash("Disabled");
    private int normalHash = Animator.StringToHash("Normal");

    private GameObject _manager;

    void Start()
    {
        _state = 1;
        _manager = GameObject.FindGameObjectWithTag("Manager");
        anim = GetComponent<Animator>();
    }

    public void setupGraphics()
    {
        _card = _manager.GetComponent<MatchingManager>().getCard();
        _carded = _manager.GetComponent<MatchingManager>().getCarded();

        clickCard();
    }

    public void clickCard()
    {
        if(_state == 0)
        {
            _state = 1;
        }
        else if(_state == 1)
        {
            _state = 0;
        }

        if (_state == 0 && !DO_NOT)
        {
            GetComponent<Image>().sprite = _card;
            anim.SetTrigger(normalHash);
        }
        else if (_state == 1 && !DO_NOT)
        {
            GetComponent<Image>().sprite = _carded;
            anim.SetTrigger(highlightedHash);
        }
    }

    public int cardValue
    {
        get { return _cardValue; }
        set { _cardValue = value; } 
    }

    public int state
    {
        get { return _state; }
        set { _state = value; }
    }

    public bool initialized
    {
        get { return _initialized; }
        set { _initialized = value; }
    }
    
    public int side
    {
        get { return _side; }
        set { _side = value; }
    }

    public void falseCheck()
    {
        StartCoroutine(pause());
    }

    IEnumerator pause()
    {
        yield return new WaitForSeconds(0.5f);
        if(_state == 0)
        {
            GetComponent<Image>().sprite = _card;
            anim.SetTrigger(normalHash);
        }
        else if(_state == 1)
        {
            GetComponent<Image>().sprite = _carded;
        }
        DO_NOT = false;
    }
}
