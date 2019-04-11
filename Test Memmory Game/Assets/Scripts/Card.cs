﻿using System.Collections;
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

    private GameObject _manager;

    void Start()
    {
        _state = 1;
        _manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void setupGraphics()
    {
        _card = _manager.GetComponent<GameManager>().getCard();
        _carded = _manager.GetComponent<GameManager>().getCarded();

        clickCard();
    }

    public void clickCard()
    {
        if(_state == 0)
        {
            _state = 1;
        }else if(_state == 1)
        {
            _state = 0;
        }

        if (_state == 0 && !DO_NOT)
        {
            GetComponent<Image>().sprite = _card;
        }
        else if (_state == 1 && !DO_NOT)
        {
            GetComponent<Image>().sprite = _carded;          
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
        yield return new WaitForSeconds(1);
        if(_state == 0)
        {
            GetComponent<Image>().sprite = _card;
        }else if(_state == 1)
        {
            GetComponent<Image>().sprite = _carded;
        }
        DO_NOT = false;
    }
}
