using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class WeaponSystem : MonoBehaviour
{
    public List<WeaponHandler> weaponsHold;
    private List<IWeapon> _weaponsRightPosition = new List<IWeapon>();
    private List<IWeapon> _weaponsLeftPosition = new List<IWeapon>();


    public bool FireWeapons(WeaponHandler.WeaponLocation WeaponLocation, Vector3 TargetAimPoint)
    {
        bool _ret = false;
        if (WeaponLocation == WeaponHandler.WeaponLocation.right)
        {
            foreach (IWeapon w in _weaponsRightPosition)
            {
                if (w.OnShoot(TargetAimPoint))
                {
                    _ret = true;
                }
            }
        }
        else if (WeaponLocation == WeaponHandler.WeaponLocation.left)
        {
            foreach (IWeapon w in _weaponsLeftPosition)
            {
                if (w.OnShoot(TargetAimPoint))
                {
                    _ret = true;
                }
            }
        }
        return _ret;
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
