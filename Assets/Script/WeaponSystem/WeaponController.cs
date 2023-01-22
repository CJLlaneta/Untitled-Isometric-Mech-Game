using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponController : MonoBehaviour
{
    [SerializeField] List<WeaponHandler> _weaponsHold;
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] GameObject _owner;
    private InputControls _playerControls;
    private List<IWeapon> _weaponsRightPosition = new List<IWeapon>();
    private List<IWeapon> _weaponsLeftPosition  = new List<IWeapon>();
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
        _weaponsRightPosition.Clear();
        _weaponsLeftPosition.Clear();
        foreach (WeaponHandler wh in _weaponsHold)
        {
            wh.SetTheOwner(_owner);
            if (wh.weaponLocation == WeaponHandler.WeaponLocation.right)
            {
                _weaponsRightPosition.Add(wh);
            }
            else if (wh.weaponLocation == WeaponHandler.WeaponLocation.left)
            {
                _weaponsLeftPosition.Add(wh);
            }
        }

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
    private void FireWeapons(List<IWeapon> weapon)
    {
        foreach(IWeapon w in weapon)
        {
            w.OnShoot();
        }
    }
    private void HandleInput()
    {
        if (_playerControls.Player.FiringRight.IsPressed())
        {
           //Debug.Log("Firing Right weapon");
           FireWeapons(_weaponsRightPosition);
        }
        if (_playerControls.Player.FiringLeft.IsPressed())
        {
            //Debug.Log("Firing Left weapon");
            FireWeapons(_weaponsLeftPosition);
        }
    }
}
