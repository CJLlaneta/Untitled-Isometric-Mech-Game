using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponSystem : MonoBehaviour
{
    public List<WeaponHandler> weaponsHold;
    private List<IWeapon> _listRightArmWeapons = new List<IWeapon>();
    private List<IWeapon> _listLeftArmWeapons = new List<IWeapon>();


    public bool FireWeapons(WeaponHandler.WeaponLocation WeaponLocation, 
        Vector3 TargetAimPoint)
    {
        if (WeaponLocation == WeaponHandler.WeaponLocation.right)
        {
           var hasfiredWeapon = _listRightArmWeapons.Select(w => w.OnShoot(TargetAimPoint));
            return hasfiredWeapon.Any(result => result);
        }
        else if (WeaponLocation == WeaponHandler.WeaponLocation.left)
        {
            var hasfiredWeapon = _listLeftArmWeapons.Select(w => w.OnShoot(TargetAimPoint));
            return hasfiredWeapon.Any(result => result);
        }
        return false;
    }
    public void InitializeProperties(GameObject owner)
    {
        _listRightArmWeapons.Clear();
        _listLeftArmWeapons.Clear();
        foreach (WeaponHandler wh in weaponsHold)
        {
            wh.SetTheOwner(owner);
            if (wh.weaponLocation == WeaponHandler.WeaponLocation.right)
            {
                _listRightArmWeapons.Add(wh);
            }
            else if (wh.weaponLocation == WeaponHandler.WeaponLocation.left)
            {
                _listLeftArmWeapons.Add(wh);
            }
        }
    }

}
