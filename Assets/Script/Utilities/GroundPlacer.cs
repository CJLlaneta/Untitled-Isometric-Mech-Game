using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacer : MonoBehaviour
{
    public LayerMask groundLayer;
    public float maxDistance = 2f;
    [SerializeField] private Vector3 _offSett = Vector3.zero;
    void Start()
    {
        SetToGround();
    }
    private void SetToGround() 
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, maxDistance, groundLayer))
        {
            Vector3 _pos = hit.point;
            _pos += _offSett;
            transform.position = _pos;
            //transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }
}
