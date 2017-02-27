using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompSciPuzzleManager : MonoBehaviour
{
    public GameObject[] TopRowPositions,
                        BottomRowPositions;
                        
    void Start()
    {
        InstantiateTopRow();
    }

    private void InstantiateTopRow()
    {
        GameObject[] SymbolPrefabs = new GameObject[4];

        for (int i = 0; i < 4; i++)
        {
            string path = "Prefabs/EncounterPuzzles/CompSciPuzzle/CSsymbol" + (i + 1);
            SymbolPrefabs[i] = Resources.Load<GameObject>(path);
        }

        for (int i = 0; i < TopRowPositions.Length; i++)
        {
            int rand = Random.Range(0, 4);
            Instantiate(SymbolPrefabs[rand], TopRowPositions[i].transform.position, Quaternion.identity);
        }
    }
}