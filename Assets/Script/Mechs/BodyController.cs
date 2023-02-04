using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class BodyController : MonoBehaviour
{
    [SerializeField] GameObject _upperBody;
    [SerializeField] GameObject _chasis;
    [SerializeField] GameObject _cockPit;

    [SerializeField] Animator _animator;

    [SerializeField] float _movementSpeed = 10;
    [SerializeField] float _movementSpeed_rotation = 5;

    [SerializeField] float _upperBodyRotationSpeed = 5;
    [SerializeField] float _chasisRotationSpeed = 5;
    
   // [SerializeField] SoundController _chasisSoundController;
    [SerializeField] SoundController _upperBodyRotationSoundController;
    public WeaponSystem weaponSystem;

    private float _currentMovementSpeed = 0;
    private void Start()
    {
        Initialized();
    }

    public void SetTheWeaponOwner(GameObject owner) 
    {
        weaponSystem.InitializeProperties(owner);
    }

    public bool FiringWeapon(WeaponHandler.WeaponLocation weaponLocation, Vector3 aimPoint) 
    {
       return weaponSystem.FireWeapons(weaponLocation, aimPoint);
        //_weaponSystem.FireWeapons(WeaponHandler.WeaponLocation.right, _playerAim.lastPoint)
    }

    public void ShutDown() 
    {
        CockPitShutDown();
        IsMoving(false);
        if (_animator != null) 
        {
            _animator.enabled = false;
        }

        Destroy(this);
    }
    private void CockPitShutDown()
    {
        Renderer renderer = _cockPit.GetComponent<Renderer>();
        Material[] _mats = renderer.materials;
        foreach (Material m in _mats) 
        {
            //Debug.Log(m.name);
            m.SetColor("_EmissionColor", Color.black);
        }
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
        if (_upperBody.transform.rotation != newRotation) 
        {
            _upperBodyRotationSoundController.TriggerSound();
        }
    }
    public void IsMoving(bool state)
    {
        if (_animator != null) 
        {
            AnimationManager.Instance.SetAnimationBoolean(_animator, "isIdle", !state);
        }

        if (state) 
        {
            if (_animator != null)
            {
                _animator.speed = _currentMovementSpeed / 5;
            }
                //_chasisSoundController.TriggerSound();
        }
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
