﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CompSciPuzzleManager : MonoBehaviour
{
    public GameObject[] TopRowPositions,
                        BottomRowPositions;
    public int Strikes;

    private GameObject[] _symbolPrefabs,
                         _grayedSymbolPrefabs;
    private Vector2[] _strikeLocs;
    private int[] _symbolOrder;
    private Dictionary<CardinalDirections, int> _arrowKeyOrder = new Dictionary<CardinalDirections, int>();
    private GameObject _incorrectSymbol,
                       _strikeObj;
    private int _currentRowIndex,
                _strikesCount;
    private bool _inputDisabled;
                        
    void Start()
    {
        _symbolOrder = new int[TopRowPositions.Length];
        InstantiateTopRow();
        InstantiateStrikes();
    }

    void Update()
    {
        if (!_inputDisabled)
            InputHandler();
    }

    private void InstantiateTopRow()
    {
        _symbolPrefabs = new GameObject[TopRowPositions.Length];
        _grayedSymbolPrefabs = new GameObject[TopRowPositions.Length];

        for (int i = 0; i < TopRowPositions.Length; i++)
        {
            string path = "Prefabs/EncounterPuzzles/CompSciPuzzle/CSsymbol" + (i + 1);

            string path2 = "Prefabs/EncounterPuzzles/CompSciPuzzle/CSGrayedSymbol" + (i + 1);

            int newRand = Random.Range(1, 5);
            while (_arrowKeyOrder.ContainsKey((CardinalDirections)newRand))
            {
                newRand = Random.Range(1, 5);
            }
            _arrowKeyOrder.Add((CardinalDirections)newRand, i);

            _symbolPrefabs[i] = Resources.Load<GameObject>(path);
            _grayedSymbolPrefabs[i] = Resources.Load<GameObject>(path2);
        }

        for (int i = 0; i < TopRowPositions.Length; i++)
        {
            int rand = Random.Range(0, 4);
            if (i == 0)
                Instantiate(_symbolPrefabs[rand], TopRowPositions[i].transform.position, Quaternion.identity);
            else
                Instantiate(_grayedSymbolPrefabs[rand], TopRowPositions[i].transform.position, Quaternion.identity);

            _symbolOrder[i] = rand;
        }
    }

    private void InstantiateStrikes()
    {
        string path = "Prefabs/EncounterPuzzles/CompSciPuzzle/StrikeSet" + Strikes;
        _strikeLocs = new Vector2[Strikes];
        GameObject strikes = Resources.Load<GameObject>(path);
        strikes = Instantiate(strikes, GameObject.Find("StrikesPos").transform.position, Quaternion.identity);
        for (int i = 0; i < Strikes; i++)
        {
            _strikeLocs[i] = strikes.transform.GetChild(i).position;
        }
        _strikeObj = Resources.Load<GameObject>("Prefabs/EncounterPuzzles/CompSciPuzzle/CSstrike");
    }

    private void InputHandler()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_arrowKeyOrder[CardinalDirections.FORWARD] == _symbolOrder[_currentRowIndex])
            {
                SuccessfulButtoPress();
            }
            else
            {
                UnsuccessfulButtonPress(CardinalDirections.FORWARD);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_arrowKeyOrder[CardinalDirections.BACKWARD] == _symbolOrder[_currentRowIndex])
            {
                SuccessfulButtoPress();
            }
            else
            {
                UnsuccessfulButtonPress(CardinalDirections.BACKWARD);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_arrowKeyOrder[CardinalDirections.RIGHT] == _symbolOrder[_currentRowIndex])
            {
                SuccessfulButtoPress();
            }
            else
            {
                UnsuccessfulButtonPress(CardinalDirections.RIGHT);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_arrowKeyOrder[CardinalDirections.LEFT] == _symbolOrder[_currentRowIndex])
            {
                SuccessfulButtoPress();
            }
            else
            {
                UnsuccessfulButtonPress(CardinalDirections.LEFT);
            }
        }
    }

    private void SuccessfulButtoPress()
    {
        Instantiate(_symbolPrefabs[_symbolOrder[_currentRowIndex]], BottomRowPositions[_currentRowIndex].transform.position, Quaternion.identity);
        _currentRowIndex++;
        if (_currentRowIndex < TopRowPositions.Length)
        {
            Instantiate(_symbolPrefabs[_symbolOrder[_currentRowIndex]], TopRowPositions[_currentRowIndex].transform.position, Quaternion.identity);
        }
        else
        {
            YouWin();
        }
    }

    private void UnsuccessfulButtonPress(CardinalDirections buttonType)
    {
        _incorrectSymbol = Instantiate(_symbolPrefabs[_arrowKeyOrder[buttonType]], BottomRowPositions[_currentRowIndex].transform.position, Quaternion.identity);
        Invoke("RemoveIncorrectSymbol", 1);
        Instantiate(_strikeObj, _strikeLocs[_strikesCount], Quaternion.identity);
        _strikesCount++;
        _inputDisabled = true;

        if (_strikesCount == 3)
            YouLose();
    }

    private void RemoveIncorrectSymbol()
    {
        _inputDisabled = false;
        GameObject.Destroy(_incorrectSymbol);
    }

    private void YouLose()
    {
        Debug.Log("NOOOOOOOOOOOO!!!");
    }

    public void YouWin()
    {
        Debug.Log("WOOOOOOOOOOOO!!!");
    }
}