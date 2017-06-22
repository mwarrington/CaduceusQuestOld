using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocArrowController : MonoBehaviour
{
    public int Index;
    public float Speed;
    public CardinalDirections MyButtonType;
    private DoctorPuzzleManager _myDocPuzzleManager;
    private Vector2 _targetLoc,
                    _movementVector;
    private float _maxDistance;
    protected bool atTarget
    {
        get
        {
            return _atTarget;
        }

        set
        {
            if(_atTarget != value)
            {
                if(value == false)
                {
                    if (_myDocPuzzleManager.KeyStrokeCount == Index)
                        _myDocPuzzleManager.EndGame();

                    GameObject.Destroy(gameObject);
                }

                _atTarget = value;
            }
        }
    }
    private bool _atTarget = false;

    private void Start()
    {
        _myDocPuzzleManager = FindObjectOfType<DoctorPuzzleManager>();
        _targetLoc = _myDocPuzzleManager.transform.position;
        _movementVector = new Vector2(_targetLoc.x - transform.position.x, _targetLoc.y - transform.position.y);

        _maxDistance = Vector2.Distance(transform.position, _targetLoc);
    }

    void Update()
    {
        //We might need something here for movement type if that ends up being a thing
        SimpleMoveAt();

        if (atTarget)
        {
            switch (MyButtonType)
            {
                case CardinalDirections.FORWARD:
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        SuccessfulButtonPress();
                    }
                    break;
                case CardinalDirections.BACKWARD:
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        SuccessfulButtonPress();
                    }
                    break;
                case CardinalDirections.RIGHT:
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        SuccessfulButtonPress();
                    }
                    break;
                case CardinalDirections.LEFT:
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        SuccessfulButtonPress();
                    }
                    break;
            }
        }
    }

    private void SimpleMoveAt()
    {
        transform.Translate(_movementVector * Time.deltaTime * Speed);

        if ((Vector2.Distance(transform.position, _targetLoc) < (Speed / 0.444444f) && (MyButtonType == CardinalDirections.BACKWARD || MyButtonType == CardinalDirections.FORWARD)) ||
                 (Vector2.Distance(transform.position, _targetLoc) < (Speed / 0.25f) && (MyButtonType == CardinalDirections.RIGHT || MyButtonType == CardinalDirections.LEFT)))
        {
            atTarget = true;
        }
        else
        {
            atTarget = false;
        }
    }

    private void SuccessfulButtonPress()
    {
        _myDocPuzzleManager.CorrectKeyPressCount++;
        if (_myDocPuzzleManager.KeyStrokeCount == Index)
            _myDocPuzzleManager.EndGame();
        GameObject.Destroy(gameObject);
    }
}