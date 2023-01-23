using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    //[SerializeField] TwinStickMovement _twinStickMovement;
    public Vector3 lastPoint;

    public void SetLastPoint(Vector3 val)
    {
        lastPoint = val;
    }
}
