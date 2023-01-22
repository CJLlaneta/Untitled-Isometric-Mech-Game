using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    //[SerializeField] TwinStickMovement _twinStickMovement;
    Vector3 _lastPoint;

    public void SetLastPoint(Vector3 val)
    {
        _lastPoint = val;
    }
}
