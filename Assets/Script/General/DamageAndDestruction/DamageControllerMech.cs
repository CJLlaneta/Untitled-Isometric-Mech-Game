using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DamageControllerMech : MonoBehaviour
{
    [SerializeField] List<DamageSystem> _damageSystems = new List<DamageSystem>();


    void Start()
    {
        
    }

    public float GetTotalHealth() 
    {
        return _damageSystems.Sum(hp => hp.GetCurrentHP());
    }

    void Update()
    {
        
    }
}
