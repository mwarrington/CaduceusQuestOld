using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorPuzzleManager : MonoBehaviour
{
    public string Name;
    public float FailPenalty;
    public float MinArrowSpeed,
                 MaxArrowSpeed,
                 SpawnRate;
    public int KeyStrokeCount;
    public bool ComplexSpawning,
                RandomSpawning;

    public int CorrectKeyPressCount
    {
        get
        {
            return _correctKeyPressCount;
        }

        set
        {
            if(value != _correctKeyPressCount)
            {
                if (value > _correctKeyPressCount)
                {
                    ToggleCorrectIndicator(true);
                    Invoke("ToggleCorrectIndicator", 0.2f);
                }
                _correctKeyPressCount = value;
            }
        }
    }
    private int _correctKeyPressCount;

    protected float successRate
    {
        get
        {
            return KeyStrokeCount / CorrectKeyPressCount;
        }
    }

    private EncounterManager _theEncounterManager;
    private Camera _myCamera;
    private SpriteRenderer _correctKeyPressIndicator,
                           _incorrectKeyPressIndicator;
    private Vector2 _topSpawnPoint,
                    _bottomSpawnPoint,
                    _leftSpawnPoint,
                    _rightSpawnPoint;
    private int _arrowSpawnCount;
    private bool _puzzleStarted;
    
    void Start()
    {
        _myCamera = GetComponentInChildren<Camera>();
        _theEncounterManager = FindObjectOfType<EncounterManager>();
        _topSpawnPoint = GameObject.Find("SpawnPointTop").transform.position;
        _bottomSpawnPoint = GameObject.Find("SpawnPointBottom").transform.position;
        _leftSpawnPoint = GameObject.Find("SpawnPointLeft").transform.position;
        _rightSpawnPoint = GameObject.Find("SpawnPointRight").transform.position;
        _correctKeyPressIndicator = GameObject.Find("target_green").GetComponent<SpriteRenderer>();
        _incorrectKeyPressIndicator = GameObject.Find("target_red").GetComponent<SpriteRenderer>();
        _correctKeyPressIndicator.enabled = false;
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            _puzzleStarted = true;

        if(_puzzleStarted)
        {
            if(_arrowSpawnCount == 0)
            {
                SimpleArrowSpawn();
            }
            else if(successRate > .9f)
            {
                _theEncounterManager.PuzzleWin(Name, this.gameObject);
            }
            else
            {
                _theEncounterManager.PuzzleFail(FailPenalty, this.gameObject);
            }
        }
    }

    private void SimpleArrowSpawn()
    {
        string path = "Prefabs/EncounterPuzzles/Doctor/arrow_";
        int rand = Random.Range(0, 4);
        Vector2 spawnPoint = Vector2.zero,
                spawnVariance = Vector2.zero;

        if (ComplexSpawning)
        {
            spawnVariance = _myCamera.ScreenToWorldPoint(new Vector3(_myCamera.pixelWidth, _myCamera.pixelHeight));
            spawnVariance = new Vector2(spawnVariance.x / 2, spawnVariance.y / 2);
        }

        if (RandomSpawning)
        {
            int newRand = Random.Range(0, 4);

            if (newRand == 0)
            {
                spawnPoint = _rightSpawnPoint + new Vector2(0, Random.Range(-spawnVariance.y, spawnVariance.y));
            }
            else if (newRand == 1)
            {
                spawnPoint = _leftSpawnPoint + new Vector2(0, Random.Range(-spawnVariance.y, spawnVariance.y));
            }
            else if (newRand == 2)
            {
                spawnPoint = _topSpawnPoint + new Vector2(Random.Range(-spawnVariance.x, spawnVariance.x), 0);
            }
            else if (newRand == 3)
            {
                spawnPoint = _bottomSpawnPoint + new Vector2(Random.Range(-spawnVariance.x, spawnVariance.x), 0);
            }

            if(rand == 0)
            {
                path += "up";
            }
            else if(rand == 1)
            {
                path += "down";
            }
            else if(rand == 2)
            {
                path += "left";
            }
            else if(rand == 3)
            {
                path += "right";
            }
        }
        else
        {
            if (rand == 0)
            {
                spawnPoint = _topSpawnPoint + new Vector2(Random.Range(-spawnVariance.x, spawnVariance.x), 0);
                path += "up";
            }
            else if (rand == 1)
            {
                spawnPoint = _bottomSpawnPoint + new Vector2(Random.Range(-spawnVariance.x, spawnVariance.x), 0);
                path += "down";
            }
            else if (rand == 2)
            {
                spawnPoint = _leftSpawnPoint + new Vector2(0, Random.Range(-spawnVariance.y, spawnVariance.y));
                path += "left";
            }
            else if (rand == 3)
            {
                spawnPoint = _rightSpawnPoint + new Vector2(0, Random.Range(-spawnVariance.y, spawnVariance.y));
                path += "right";
            }
        }

        GameObject arrow = Resources.Load<GameObject>(path);
        arrow = Instantiate(arrow, spawnPoint, Quaternion.identity);
        arrow.GetComponent<DocArrowController>().Speed = Random.Range(MinArrowSpeed, MaxArrowSpeed);
        _arrowSpawnCount++;

        if (_arrowSpawnCount < KeyStrokeCount)
            Invoke("SimpleArrowSpawn", SpawnRate);
    }

    private void GameEndHandler()
    {

    }

    //private void ComplexArrowSpawn()
    //{
    //    string path = "Prefabs/EncounterPuzzles/Doctor/arrow_";
    //    int rand = Random.Range(0, 4);
    //    Vector2 spawnPoint = Vector2.zero,
    //            spawnVariance = _myCamera.ScreenToWorldPoint(new Vector3(_myCamera.pixelWidth, _myCamera.pixelHeight));

    //    spawnVariance = new Vector2(spawnVariance.x / 2, spawnVariance.y / 2);

    //    if (rand == 0)
    //    {
    //        spawnPoint = _topSpawnPoint + new Vector2(Random.Range(-spawnVariance.x, spawnVariance.x), 0);
    //        path += "up";
    //    }
    //    else if (rand == 1)
    //    {
    //        spawnPoint = _bottomSpawnPoint + new Vector2(Random.Range(-spawnVariance.x, spawnVariance.x), 0); ;
    //        path += "down";
    //    }
    //    else if (rand == 2)
    //    {
    //        spawnPoint = _leftSpawnPoint + new Vector2(0, Random.Range(-spawnVariance.y, spawnVariance.y));
    //        path += "left";
    //    }
    //    else if (rand == 3)
    //    {
    //        spawnPoint = _rightSpawnPoint + new Vector2(0, Random.Range(-spawnVariance.y, spawnVariance.y));
    //        path += "right";
    //    }

    //    GameObject arrow = Resources.Load<GameObject>(path);
    //    arrow = Instantiate(arrow, spawnPoint, Quaternion.identity);
    //    arrow.GetComponent<DocArrowController>().Speed = Random.Range(MinArrowSpeed, MaxArrowSpeed);
    //    _arrowSpawnCount++;

    //    if (_arrowSpawnCount < KeyStrokeCount)
    //        Invoke("ComplexArrowSpawn", SpawnRate);
    //}

    private void ToggleCorrectIndicator()
    {
        _correctKeyPressIndicator.enabled = !_correctKeyPressIndicator.enabled;
    }

    private void ToggleCorrectIndicator(bool turnOn)
    {
        _correctKeyPressIndicator.enabled = turnOn;
        CancelInvoke("ToggleCorrectIndicator");
    }
}