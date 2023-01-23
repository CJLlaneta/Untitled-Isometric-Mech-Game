using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField] GameObject _upperBody;
    [SerializeField] GameObject _chasis;
    [SerializeField] Animator _animator;

    [SerializeField] float _movementSpeed = 10;
    [SerializeField] float _movementSpeed_rotation = 5;

    [SerializeField] float _upperBodyRotationSpeed = 5;
    [SerializeField] float _chasisRotationSpeed = 5;

    private float _currentMovementSpeed = 0;
    private void Start()
    {
        Initialized();
    }
    private void Initialized()
    {
        _currentMovementSpeed = _movementSpeed;
        IsMoving(false);
    }
    public float MovementSpeed
    {   
        get { return _currentMovementSpeed; } 
    }
    public void UpperRotation(Quaternion newRotation)
    {
        Quaternion _finalRotation = Quaternion.RotateTowards(_upperBody.transform.rotation,newRotation,_upperBodyRotationSpeed * Time.deltaTime);
        _upperBody.transform.rotation = _finalRotation;
    }
    public void IsMoving(bool state)
    {
        AnimationManager.Instance.SetAnimationBoolean(_animator, "isIdle", !state);
    }

    public void ChasisRotation(Quaternion newRotation)
    {
        Quaternion _finalRotation = Quaternion.RotateTowards(_chasis.transform.rotation,newRotation,_chasisRotationSpeed * Time.deltaTime);
        _chasis.transform.rotation = _finalRotation;
        if (_chasis.transform.rotation != newRotation) 
        {
            _currentMovementSpeed = _movementSpeed_rotation;
        }
        else if (_chasis.transform.rotation == newRotation)
        {
            _currentMovementSpeed = _movementSpeed;
        }
    }

    
}
