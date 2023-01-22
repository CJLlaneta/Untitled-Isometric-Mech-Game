using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolHandler : MonoBehaviour
{
    [SerializeField] List<PoolProperties> _bulletsObject;
    [SerializeField] List<PoolProperties> _muzzleObject;
    [SerializeField] List<PoolProperties> _hitObject;
    void Start()
    {
        ObjectPoolingManager.Instance.InitilializePool(_bulletsObject);
        ObjectPoolingManager.Instance.InitilializePool(_muzzleObject);
        ObjectPoolingManager.Instance.InitilializePool(_hitObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
