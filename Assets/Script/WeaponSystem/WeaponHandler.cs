using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour,IWeapon
{
  
    [SerializeField] WeaponProperties _weaponProperties;
    [SerializeField] SoundController _soundController;
    public WeaponLocation weaponLocation = WeaponLocation.right;
    [SerializeField] Animator _animator;
    private int _clipCapacity;
    private int _defualAmmoCapacity;
    private float _rateOfFire;
    private string _projectileID;
    private string _muzzleID;
    private float _damage = 0;
    private bool _allowedToFire =true;
    private GameObject _owner;

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
        AnimationManager.Instance.PlayClip(_animator, "Shoot");
    }
    private void SpawnMuzzle() 
    {
        ObjectPoolingManager.Instance.SpawnFromPool(_muzzleID, _weaponProperties.muzzlePoint.position, _weaponProperties.transform.rotation);
    }
    private void SpawnProjectile(Vector3 TargetPoint)
    {
        PlayShootAnimation();
        _currentProjectile = ObjectPoolingManager.Instance.SpawnFromPool(_projectileID,_weaponProperties.muzzlePoint.position, _weaponProperties.transform.rotation);

        _currentProjectile.GetComponent<ProjectileCollisionDetector>().SetProperties(_owner, _damage);
        _currentProjectile.transform.LookAt(TargetPoint);
        _eulerAngles = _currentProjectile.transform.rotation.eulerAngles;
        _eulerAngles.y = _weaponProperties.transform.eulerAngles.y;
        _eulerAngles.z = _weaponProperties.transform.eulerAngles.z;
        _currentProjectile.transform.rotation = Quaternion.Euler(_eulerAngles);
  
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
