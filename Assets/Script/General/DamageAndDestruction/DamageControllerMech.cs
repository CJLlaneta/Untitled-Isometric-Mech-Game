using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

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
        if (!_EnemyMechs.IsInEngageMode())
        {
            SetAgressive();
        }

    }
    private void SetAgressive() 
    {
        _EnemyMechs.SetEngageMode(true);
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
