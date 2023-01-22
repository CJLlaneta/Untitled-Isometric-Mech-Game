using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class TwinStickMovement : MonoBehaviour
{

    [SerializeField] private float _gravityValue = -9.81f;
    [SerializeField] private float _controllerDeadZone= 0.1f;

    [SerializeField] PlayerAim _playerAim;
    [SerializeField] BodyControllerPlayer _bodyController;

    private static bool _isGamePad;

    CharacterController _characterController;

    private Vector2 _movement;
    private Vector2 _aim;

    private Vector3 _playerVelocity;
    private InputControls _playerControls;
    private PlayerInput _playerInput;
    void Awake()
    {
        _characterController = gameObject.GetComponent<CharacterController>();
        _playerControls = new InputControls();
        _playerInput = GetComponent<PlayerInput>();
    }
    public static bool IsGamePad
    {
        get{ return _isGamePad;}
    }
    
    void OnEnable()
    {
        _playerControls.Enable();
    }
    void OnDisable()
    {
        _playerControls.Disable();
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        AimRotation();
    }

    void HandleInput()
    {
        _movement = _playerControls.Player.Movement.ReadValue<Vector2>();
        _aim = _playerControls.Player.Aim.ReadValue<Vector2>();
    }

    void HandleMovement()
    {
        Vector3 move = new Vector3(_movement.x,0,_movement.y);
        _characterController.Move(move * Time.deltaTime * _bodyController.MovementSpeed);

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
        if (_movement.x != 0 || _movement.y != 0)
        {
            LookAtMovement(move);
        }
  
    }

    private void SetPlayerAim(Vector3 point)
    {
        _playerAim.SetLastPoint(point);
    }

    void AimRotation()
    {
        if (_isGamePad)
        {
            if (Mathf.Abs(_aim.x) > _controllerDeadZone || Mathf.Abs(_aim.y) > _controllerDeadZone)
            {
                Vector3 playerDirection = Vector3.right * _aim.x + Vector3.forward * _aim.y;
               
                if (playerDirection.sqrMagnitude >0.0f)
                {
                    // Quaternion newRotation = Quaternion.LookRotation(playerDirection,Vector3.up);
                    // transform.rotation = Quaternion.RotateTowards(transform.rotation,newRotation,_rotationSpeed * Time.deltaTime);
                    LookAt_Controller(playerDirection,PartMovement.UpperBody);
                    SetPlayerAim(transform.forward);
                }
            }
        }
        else 
        {
            Ray ray = Camera.main.ScreenPointToRay(_aim);
            Plane groundPlane = new Plane(Vector3.up,Vector3.zero);
            float rayDistance;
            if (groundPlane.Raycast(ray,out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt_Mouse(point,PartMovement.UpperBody);
                SetPlayerAim(point);
            }
        }
    }


    private void RotateParts(Quaternion newRotation,PartMovement part)
    {
        if (part == PartMovement.UpperBody)
        {
            _bodyController.UpperRotation(newRotation);
        }
        else 
        {
            _bodyController.ChasisRotation(newRotation);
        }
    }

    private void LookAtMovement(Vector3 direction)
    {
        Quaternion newRotation = Quaternion.LookRotation(direction,Vector3.up);
        RotateParts(newRotation,PartMovement.Chasis); 
    }
    private void LookAt_Controller(Vector3 point,PartMovement part)
    {
        Quaternion newRotation = Quaternion.LookRotation(point,Vector3.up);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation,newRotation,_rotationSpeed * Time.deltaTime);
        RotateParts(newRotation,part);
    }
    private void LookAt_Mouse(Vector3 point,PartMovement part)
    {
        Vector3 heightCorrectedPoint = new Vector3(point.x,transform.position.y,point.z);
        Vector3 relativePos = heightCorrectedPoint- transform.position;
        Quaternion newRotation = Quaternion.LookRotation(relativePos);
        RotateParts(newRotation,part);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation,newRotation,_rotationSpeed * Time.deltaTime);
    }


    public void OnDeviceChange(PlayerInput pi)
    {
        _isGamePad = pi.currentControlScheme.Equals("Gamepad") ? true :false;
    }

    public enum PartMovement
    {
        Chasis,
        UpperBody
    }
}
