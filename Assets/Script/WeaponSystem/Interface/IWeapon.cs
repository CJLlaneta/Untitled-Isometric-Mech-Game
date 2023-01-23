using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface IWeapon 
{
    void OnReload();
    void OnShoot(Vector3 Target);
    void SetTheOwner(GameObject owner);
}
