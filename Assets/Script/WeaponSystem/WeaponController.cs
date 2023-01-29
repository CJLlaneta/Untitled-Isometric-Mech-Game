using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponController : MonoBehaviour
{

    //[SerializeField] WeaponSystem _weaponSystem;
    [SerializeField] BodyController _bodyController;
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] GameObject _owner;
    [SerializeField] PlayerAim _playerAim;
    [SerializeField] RecoilShake _recoilShake;
    private InputControls _playerControls;

    void Awake()
    {
        _playerControls = new InputControls();
    }
    void Start()
    {
        InitializeProperties();
    }
    void InitializeProperties()
    {
        _bodyController.weaponSystem.InitializeProperties(_owner);
       // _weaponSystem.InitializeProperties(_owner);
    }
    void OnEnable()
    {
        _playerControls.Enable();
    }
    void OnDisable()
    {
        _playerControls.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (_playerControls.Player.FiringRight.IsPressed())
        {
            //_bodyController.FiringWeapon(WeaponHandler.WeaponLocation.right, _playerAim.lastPoint)
            // _weaponSystem.FireWeapons(WeaponHandler.WeaponLocation.right, _playerAim.lastPoint)
           if (_bodyController.FiringWeapon(WeaponHandler.WeaponLocation.right, _playerAim.lastPoint)) 
           {
                _recoilShake.Recoil();
           }
        }
        if (_playerControls.Player.FiringLeft.IsPressed())
        {
            if (_bodyController.FiringWeapon(WeaponHandler.WeaponLocation.left, _playerAim.lastPoint))
            {
                _recoilShake.Recoil();
            }
           
        }
    }
}
