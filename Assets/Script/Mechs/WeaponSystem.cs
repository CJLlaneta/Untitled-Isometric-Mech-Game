using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponSystem : MonoBehaviour
{
    public List<WeaponHandler> weaponsHold;
    private List<IWeapon> _weaponsRightPosition = new List<IWeapon>();
    private List<IWeapon> _weaponsLeftPosition = new List<IWeapon>();


    public bool FireWeapons(WeaponHandler.WeaponLocation WeaponLocation, 
        Vector3 TargetAimPoint)
    {
        if (WeaponLocation == WeaponHandler.WeaponLocation.right)
        {
           var _result = _weaponsRightPosition.Select(w => w.OnShoot(TargetAimPoint));
            return _result.Any(r => r);
        }
        else if (WeaponLocation == WeaponHandler.WeaponLocation.left)
        {
            var _result = _weaponsLeftPosition.Select(w => w.OnShoot(TargetAimPoint));
            return _result.Any(r => r);
        }
        return false;
    }
    public void InitializeProperties(GameObject owner)
    {
        _weaponsRightPosition.Clear();
        _weaponsLeftPosition.Clear();
        foreach (WeaponHandler wh in weaponsHold)
        {
            wh.SetTheOwner(owner);
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

}
