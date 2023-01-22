using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyControllerPlayer : MonoBehaviour
{
    [SerializeField] GameObject _upperBody;
    [SerializeField] GameObject _chasis;

    [SerializeField] float _movementSpeed = 10;

    [SerializeField] float _upperBodyRotationSpeed = 5;
    [SerializeField] float _chasisRotationSpeed = 5;

    public float MovementSpeed
    {   
        get { return _movementSpeed; } 
    }
    public void UpperRotation(Quaternion newRotation)
    {
        Quaternion _finalRotation = Quaternion.RotateTowards(_upperBody.transform.rotation,newRotation,_upperBodyRotationSpeed * Time.deltaTime);
        _upperBody.transform.rotation = _finalRotation;
    }

    public void ChasisRotation(Quaternion newRotation)
    {
        Quaternion _finalRotation = Quaternion.RotateTowards(_chasis.transform.rotation,newRotation,_chasisRotationSpeed * Time.deltaTime);
        _chasis.transform.rotation = _finalRotation;
    }
}
