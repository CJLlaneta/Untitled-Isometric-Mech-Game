using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public float healthValue = 100;

    private float _hp = 0;
    private void Start()
    {
        _hp = healthValue;
    }
    public void ApplyDamage(float Damage) 
    {
        _hp -= Damage;
        if (_hp <= 0) 
        {
            OnBreakEvent?.Invoke();
        }
        if (OnDamageEvent != null) 
        {
            OnDamageEvent.Invoke(healthValue, _hp);
        }
   

    }
    //implementation
    //DamageSystem.OnBreakEvent += BreakArmor;
    public delegate void OnDamage(float MaxHP,float CurrentHP);
    public event OnDamage OnDamageEvent;

    public delegate void OnBreak();
    public event OnBreak OnBreakEvent;
}
