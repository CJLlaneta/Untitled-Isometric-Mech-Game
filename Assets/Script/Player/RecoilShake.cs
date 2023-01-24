using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilShake : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource _impulseSource;
    [SerializeField] float _powerMount;


    public void Recoil() 
    {
        _impulseSource.GenerateImpulseWithForce(_powerMount);
    }
}
