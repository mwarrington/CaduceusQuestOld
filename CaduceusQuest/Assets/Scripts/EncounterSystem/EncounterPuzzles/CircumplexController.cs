using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircumplexController : MonoBehaviour
{
    public SpriteRenderer TopIndicator,
                          TopMiddleIndicator,
                          BottomMiddleIndicator,
                          BottomIndicator;

    protected int currentEmotionIntensity
    {
        get
        {
            return _currentEmotionIntensity;
        }

        set
        {
            if (value != _currentEmotionIntensity)
            {
                if (value == 4)
                {
                    TopIndicator.enabled = true;
                }
                else if (value == 3)
                {
                    TopMiddleIndicator.enabled = true;
                }
                else if (value == 2)
                {
                    BottomMiddleIndicator.enabled = true;
                }
                else if (value == 1)
                {
                    BottomIndicator.enabled = true;
                }

                _currentEmotionIntensity = value;
                _currentEmotion.EmotionIntensity = _currentEmotionIntensity;
            }
        }
    }
    protected char currentEmotionType
    {
        get
        {
            return _currentEmotionType;
        }

        set
        {
            _currentEmotionType = value;
            _currentEmotion.EmotionType = _currentEmotionType;
        }
    }

    private DialogPuzzleManager _myDialogPuzzMan;

    protected Emotion currentEmotion
    {
        get { return _currentEmotion = new Emotion(currentEmotionType, currentEmotionIntensity); }

        set
        {
            if (value != _currentEmotion)
            {
                _currentEmotion = value;
            }
        }
    }
    private Emotion _currentEmotion;
    
    private int _currentEmotionIntensity,
                _targetEmotionIntensity;
    private char _currentEmotionType,
                 _targetEmotionType;
    private Vector3 _lastRotation,
                    _targetRotation;
    private bool _inputEnabled = true;

    private void Start()
    {
        _currentEmotion = new Emotion('b', 4);
        _currentEmotionIntensity = 4;
        _currentEmotionType = 'b';

        _myDialogPuzzMan = this.GetComponentInParent<DialogPuzzleManager>();
        _targetEmotionIntensity = _currentEmotionIntensity;
        _targetEmotionType = _currentEmotionType;
    }

    void Update()
    {
        if (_targetEmotionType != _currentEmotionType || _targetEmotionIntensity != _currentEmotionIntensity)
            RotationHandler();

        if (_inputEnabled)
            InputHandler();
    }

    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _inputEnabled = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _lastRotation = this.transform.eulerAngles;

            _targetRotation = new Vector3(_lastRotation.x, _lastRotation.y, _lastRotation.z - 45);
            _targetEmotionType = currentEmotion.GetLastEmotionType();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _lastRotation = this.transform.eulerAngles;

            _targetRotation = new Vector3(_lastRotation.x, _lastRotation.y, _lastRotation.z + 45);
            _targetEmotionType = currentEmotion.GetNextEmotionType();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //this.transform.eulerAngles = new Vector3(transform.eulerAngles.x - (30 * Time.deltaTime), transform.eulerAngles.y, transform.eulerAngles.z);//.Rotate(Vector3.left * Time.deltaTime * 60);
            _lastRotation = this.transform.eulerAngles;

            if (currentEmotionIntensity == 4)
                _targetRotation = new Vector3(_lastRotation.x - 20, _lastRotation.y, _lastRotation.z);
            else if (currentEmotionIntensity == 3)
                _targetRotation = new Vector3(_lastRotation.x - 50, _lastRotation.y, _lastRotation.z);
            else if (currentEmotionIntensity == 2)
                _targetRotation = new Vector3(_lastRotation.x + 57, _lastRotation.y, _lastRotation.z);

            if (currentEmotionIntensity != 1)
            {
                TopIndicator.enabled = false;
                TopMiddleIndicator.enabled = false;
                BottomMiddleIndicator.enabled = false;
                BottomIndicator.enabled = false;

                _inputEnabled = false;
                _targetEmotionIntensity--;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //this.transform.eulerAngles = new Vector3(transform.eulerAngles.x - (30 * Time.deltaTime), transform.eulerAngles.y, transform.eulerAngles.z);//.Rotate(Vector3.left * Time.deltaTime * 60);
            _lastRotation = this.transform.eulerAngles;

            if (currentEmotionIntensity == 3)
                _targetRotation = new Vector3(_lastRotation.x + 20, _lastRotation.y, _lastRotation.z);
            else if (currentEmotionIntensity == 2)
                _targetRotation = new Vector3(_lastRotation.x - 50, _lastRotation.y, _lastRotation.z);
            else if (currentEmotionIntensity == 1)
                _targetRotation = new Vector3(_lastRotation.x - 57, _lastRotation.y, _lastRotation.z);

            if (currentEmotionIntensity != 4)
            {
                TopIndicator.enabled = false;
                TopMiddleIndicator.enabled = false;
                BottomMiddleIndicator.enabled = false;
                BottomIndicator.enabled = false;

                _inputEnabled = false;
                _targetEmotionIntensity++;
            }
        }

        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            _myDialogPuzzMan.SelectEmotion(currentEmotion);
        }
    }

    private void RotationHandler()
    {
        if(_targetEmotionType != _currentEmotionType)
        {
            _lastRotation = Vector3.Lerp(_lastRotation, _targetRotation, 30 * Time.deltaTime);
            this.transform.eulerAngles = _lastRotation;

            if (Quaternion.Angle(Quaternion.Euler(_lastRotation), Quaternion.Euler(_targetRotation)) < 0.1f)
            {
                currentEmotionType = _targetEmotionType;
                _inputEnabled = true;
            }
        }

        if(_targetEmotionIntensity < currentEmotionIntensity)
        {
            _lastRotation = Vector3.Lerp(_lastRotation, _targetRotation, 30 * Time.deltaTime);
            this.transform.eulerAngles = _lastRotation;

            if (Quaternion.Angle(Quaternion.Euler(_lastRotation), Quaternion.Euler(_targetRotation)) < 0.1f)
            {
                currentEmotionIntensity--;
                _inputEnabled = true;
            }
        }

        if (_targetEmotionIntensity > currentEmotionIntensity)
        {
            _lastRotation = Vector3.Lerp(_lastRotation, _targetRotation, 30 * Time.deltaTime);
            this.transform.eulerAngles = _lastRotation;

            if (Quaternion.Angle(Quaternion.Euler(_lastRotation), Quaternion.Euler(_targetRotation)) < 0.1f)
            {
                currentEmotionIntensity++;
                _inputEnabled = true;
            }
        }
    }
}