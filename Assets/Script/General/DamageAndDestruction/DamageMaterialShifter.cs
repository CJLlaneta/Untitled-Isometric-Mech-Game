using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMaterialShifter : MonoBehaviour
{
    [SerializeField] GameObject _object;
    [SerializeField] float _percentageDamage = 0.5f;
    [SerializeField] DamageSystem _damageSystem;
    [SerializeField] Material _damageMaterial;
    void Start()
    {
        _damageSystem.OnDamageEvent += DamageShifter;
    }

    private void ChangeToDamageMaterial() 
    {
        Renderer renderer = _object.GetComponent<Renderer>();
        renderer.material = _damageMaterial;
    }
    private void DamageShifter(float MaxHP,float CurrentHP) 
    {
        if (CurrentHP < (MaxHP * _percentageDamage)) 
        {
            ChangeToDamageMaterial();
            this.enabled = false;
        }
    }
}
