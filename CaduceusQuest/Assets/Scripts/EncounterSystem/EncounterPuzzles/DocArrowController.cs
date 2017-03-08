using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocArrowController : MonoBehaviour
{
    public float Speed;
    private DoctorPuzzleManager _myDocPuzzleManager;
    private Vector2 _targetLoc,
                    _movementVector;
    private bool _atTarget;

    private void Start()
    {
        _myDocPuzzleManager = FindObjectOfType<DoctorPuzzleManager>();
        _targetLoc = _myDocPuzzleManager.transform.position;
        _movementVector = new Vector2(_targetLoc.x - transform.position.x, _targetLoc.y - transform.position.y);
    }

    void Update()
    {
        //We might need something here for movement type if that ends up being a thing
        SimpleMoveAt();

        if (_atTarget)
        {
            //Start here
        }
    }

    private void SimpleMoveAt()
    {
        if (Vector2.Distance(transform.position, _targetLoc) < 0.3f)
        {
            GameObject.Destroy(this.gameObject);
        }
        else if (Vector2.Distance(transform.position, _targetLoc) < (Speed / 0.1f))
        {
            _atTarget = true;
            transform.Translate(_movementVector * Time.deltaTime * Speed);
        }
        else
        {
            transform.Translate(_movementVector * Time.deltaTime * Speed);
        }
    }
}