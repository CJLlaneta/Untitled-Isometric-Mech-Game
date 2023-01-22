using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] float _speedProjectile = 60;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         gameObject.transform.position += gameObject.transform.forward * _speedProjectile * Time.deltaTime;
    }
}
