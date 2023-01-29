using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DamageControllerMech : MonoBehaviour
{
    [SerializeField] DamageSystem _damageSystem;
    [SerializeField] float _currentHealth = 0;
    [SerializeField] EnemyMechs _EnemyMechs;
    void Start()
    {
        _damageSystem.OnDamageEvent += DamageReceive;
        _damageSystem.OnBreakEvent += ShutDownMech;
    }
    private void DamageReceive(float MaxHP, float CurrentHP)
    {
        _currentHealth = CurrentHP;

    }
    private void ShutDownMech() 
    {
        _EnemyMechs.ShutDownTheMech();
        this.enabled = false;
    }    
    public float GetMaxHealth()
    {
        return _damageSystem.healthValue;
    }
    public float GetCurrentHealth() 
    {
        return _damageSystem.GetCurrentHP();
    }

    void Update()
    {
        
    }
}
