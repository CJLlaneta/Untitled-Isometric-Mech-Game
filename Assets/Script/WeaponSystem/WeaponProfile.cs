using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable_Objects/WeaponsProfile")]
public class WeaponProfile : ScriptableObject
{
  [SerializeField] string _weaponName;
  public string WeaponName{get {return _weaponName;} private set {_weaponName =value;}}

  [SerializeField] int _clipCapacity;
  public int ClipCapacity{get {return _clipCapacity;} private set {_clipCapacity =value;}}

  [SerializeField] int _defualAmmoCapacity;
  public int DefualAmmoCapacity{get {return _defualAmmoCapacity;} private set {_defualAmmoCapacity =value;}}

  [SerializeField] float _rateOfFire;
  public float RateOfFire{get {return _rateOfFire;} private set {_rateOfFire =value;}}
  [SerializeField] string _projectileID;
  public string ProjectileID{get {return _projectileID;} private set {_projectileID =value;}}

  
}
