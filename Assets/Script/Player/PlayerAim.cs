using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    //[SerializeField] TwinStickMovement _twinStickMovement;
    public Vector3 lastPoint;
    [SerializeField] float _shortestFiringDistance = 12;
    public void SetLastPoint(Vector3 val)
    {
        if (Vector3.Distance(transform.position, val) >= _shortestFiringDistance)
        {
            lastPoint = val;
        }
    }
}
