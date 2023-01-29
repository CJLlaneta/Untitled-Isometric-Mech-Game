using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DamageControllerMech : MonoBehaviour
{
    [SerializeField] List<DamageSystem> _damageSystems = new List<DamageSystem>();
    [SerializeField] float _currentHealth = 0;

    void Start()
    {
        
    }
    public float GetMaxHealth()
    {
        return _damageSystems.Sum(hp => hp.healthValue);
    }
    public float GetCurrentHealth() 
    {
        _currentHealth = _damageSystems.Sum(hp => hp.GetCurrentHP());
        return _currentHealth;
    }

    void Update()
    {
        
    }
}
