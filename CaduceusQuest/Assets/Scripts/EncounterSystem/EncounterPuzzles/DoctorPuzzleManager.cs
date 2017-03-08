using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorPuzzleManager : MonoBehaviour
{
    public float MinArrowSpeed,
                 MaxArrowSpeed,
                 SpawnRate;
    public int KeyStrokeCount;

    private Vector2 _topSpawnPoint,
                    _bottomSpawnPoint,
                    _leftSpawnPoint,
                    _rightSpawnPoint;
    private int _arrowSpawnCount;
    private bool _puzzleStarted;
    
    void Start()
    {
        _topSpawnPoint = GameObject.Find("SpawnPointTop").transform.position;
        _bottomSpawnPoint = GameObject.Find("SpawnPointBottom").transform.position;
        _leftSpawnPoint = GameObject.Find("SpawnPointLeft").transform.position;
        _rightSpawnPoint = GameObject.Find("SpawnPointRight").transform.position;
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            _puzzleStarted = true;

        if(_puzzleStarted)
        {
            if(_arrowSpawnCount == 0)
            {
                SimpleSpawnArrow();
            }
        }
    }

    private void SimpleSpawnArrow()
    {
        string path = "Prefabs/EncounterPuzzles/Doctor/arrow_";
        int rand = Random.Range(0, 4);
        Vector2 spawnPoint = Vector2.zero;

        if (rand == 0)
        {
            spawnPoint = _topSpawnPoint;
            path += "up";
        }
        else if (rand == 1)
        {
            spawnPoint = _bottomSpawnPoint;
            path += "down";
        }
        else if (rand == 2)
        {
            spawnPoint = _leftSpawnPoint;
            path += "left";
        }
        else if (rand == 3)
        {
            spawnPoint = _rightSpawnPoint;
            path += "right";
        }

        GameObject arrow = Resources.Load<GameObject>(path);
        arrow = Instantiate(arrow, spawnPoint, Quaternion.identity);
        arrow.GetComponent<DocArrowController>().Speed = Random.Range(MinArrowSpeed, MaxArrowSpeed);
        _arrowSpawnCount++;

        if (_arrowSpawnCount < KeyStrokeCount)
            Invoke("SimpleSpawnArrow", SpawnRate);
    }
}