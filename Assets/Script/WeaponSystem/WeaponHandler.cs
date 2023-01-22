using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour,IWeapon
{
  
    [SerializeField] WeaponProperties _weaponProperties;
    public WeaponLocation weaponLocation = WeaponLocation.right;
    private int _clipCapacity;
    private int _defualAmmoCapacity;
    private float _rateOfFire;
    private string _projectileID;
    private bool _allowedToFire =true;
    private GameObject _owner;

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
    private void SpawnProjectile()
    {
       GameObject _proj = ObjectPoolingManager.Instance.SpawnFromPool(_projectileID,_weaponProperties.muzzlePoint.position,_weaponProperties.transform.rotation);
       _proj.GetComponent<ProjectileCollisionDetector>().SetTheOwner(_owner);
    }
    IEnumerator FireCooldown()
    {
        _allowedToFire =false;
        yield return new WaitForSeconds(_rateOfFire);
        _allowedToFire = true;
    }
    public void OnShoot()
    {
        if (_allowedToFire)
        {
            StartCoroutine(FireCooldown());
            SpawnProjectile();
        }    
    }
}
