using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour,IWeapon
{
  
    [SerializeField] WeaponProperties _weaponProperties;
    [SerializeField] SoundController _soundController;
    public WeaponLocation weaponLocation = WeaponLocation.right;
    [SerializeField] Animator _animator;
    [SerializeField] private GameObject _owner;
    [SerializeField] private bool _ignoreOffset = false;
    private int _clipCapacity;
    private int _defualAmmoCapacity;
    private float _rateOfFire;
    private string _projectileID;
    private string _muzzleID;
    private float _damage = 0;
    private bool _allowedToFire =true;
   

    Vector3 _eulerAngles = Vector3.zero;
    GameObject _currentProjectile;

    void Start()
    {
        InitializeProperties();
    }

    private void InitializeProperties()
    {
        _clipCapacity = _weaponProperties.weaponProfile.ClipCapacity;
        _defualAmmoCapacity = _weaponProperties.weaponProfile.DefualAmmoCapacity;
        _rateOfFire = _weaponProperties.weaponProfile.RateOfFire;
        _projectileID = _weaponProperties.weaponProfile.ProjectileID;
        _muzzleID = _weaponProperties.weaponProfile.MuzzleID;
        _damage = _weaponProperties.weaponProfile.Damage;
        _soundController.SetSoundProfile(_weaponProperties.weaponProfile.FiringSoundProfile);
    }
    public enum WeaponLocation
    {
        right,
        left
    }
    public void OnReload()
    {

    }
    public void SetTheOwner(GameObject owner)
    {
        _owner = owner;
    }

    private void PlayShootAnimation() 
    {
        if (_animator != null) 
        {
            AnimationManager.Instance.PlayClip(_animator, "Shoot");
        }

    }
    private void SpawnMuzzle() 
    {
        foreach (Transform muzzle in _weaponProperties.muzzlePoint) 
        {
            ObjectPoolingManager.Instance.SpawnFromPool(_muzzleID, muzzle.position, _weaponProperties.transform.rotation);
        }
        
    }
    private void SpawnProjectile(Vector3 TargetPoint)
    {
        PlayShootAnimation();
        foreach (Transform muzzle in _weaponProperties.muzzlePoint)
        {
            _currentProjectile = ObjectPoolingManager.Instance.SpawnFromPool(_projectileID, muzzle.position, _weaponProperties.transform.rotation);

            _currentProjectile.GetComponent<ProjectileCollisionDetector>().SetProperties(_owner, _damage);
            _currentProjectile.transform.LookAt(TargetPoint);
            if (!_ignoreOffset)
            {
                _eulerAngles = _currentProjectile.transform.rotation.eulerAngles;
                _eulerAngles.y = _weaponProperties.transform.eulerAngles.y;
                _eulerAngles.z = _weaponProperties.transform.eulerAngles.z;
                _currentProjectile.transform.rotation = Quaternion.Euler(_eulerAngles);
            }
        }
    }
    IEnumerator FireCooldown()
    {
        _allowedToFire =false;
        yield return new WaitForSeconds(_rateOfFire);
        _allowedToFire = true;
    }
    public bool OnShoot(Vector3 Target)
    {
        bool _ret = false;
        if (_allowedToFire)
        {
            StartCoroutine(FireCooldown());
            SpawnProjectile(Target);
            SpawnMuzzle();
            _soundController.TriggerSound();
            return true;
        }
        return _ret;
    }
}
