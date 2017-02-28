using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompSciPuzzleManager : MonoBehaviour
{
    public GameObject[] TopRowPositions,
                        BottomRowPositions;

    private GameObject[] _symbolPrefabs,
                         _grayedSymbolPrefabs;
    private CardinalDirections[] _correctKeys;
    private Dictionary<int, int> _keyedSymbolType = new Dictionary<int, int>();
    private int _currentRowIndex,
                _strikesCount;
                        
    void Start()
    {
        _correctKeys = new CardinalDirections[TopRowPositions.Length];
        InstantiateTopRow();
    }

    void Update()
    {
        InputHandler();
    }

    private void InstantiateTopRow()
    {
        _symbolPrefabs = new GameObject[TopRowPositions.Length];
        _grayedSymbolPrefabs = new GameObject[TopRowPositions.Length];

        for (int i = 0; i < 4; i++)
        {
            string path = "Prefabs/EncounterPuzzles/CompSciPuzzle/CSsymbol" + (i + 1);

            string path2 = "Prefabs/EncounterPuzzles/CompSciPuzzle/CSGrayedSymbol" + (i + 1);

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


            if (!_keyedSymbolType.ContainsKey(rand))
            {
                int newRand = Random.Range(0, 4);
                while (_keyedSymbolType.ContainsValue(newRand))
                {
                    newRand = Random.Range(0, 4);
                }
                _correctKeys[i] = (CardinalDirections)newRand;
                _keyedSymbolType.Add(rand, newRand);
            }
            else
            {
                _correctKeys[i] = (CardinalDirections)_keyedSymbolType[rand];
            }
        }
        _keyedSymbolType.Clear();
    }

    private void InputHandler()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_correctKeys[_currentRowIndex] == CardinalDirections.FORWARD)
                _currentRowIndex++;
            else
                _strikesCount++;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_correctKeys[_currentRowIndex] == CardinalDirections.BACKWARD)
                _currentRowIndex++;
            else
                _strikesCount++;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_correctKeys[_currentRowIndex] == CardinalDirections.RIGHT)
                _currentRowIndex++;
            else
                _strikesCount++;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_correctKeys[_currentRowIndex] == CardinalDirections.LEFT)
                _currentRowIndex++;
            else
                _strikesCount++;
        }
    }
}