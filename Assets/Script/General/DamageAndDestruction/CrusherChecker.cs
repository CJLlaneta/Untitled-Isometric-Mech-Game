using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DamageSystem;

public class CrusherChecker : MonoBehaviour
{
    [SerializeField] TagCollision _tagCollisions;
    [SerializeField] float _crushMass = 100;
    private void OnTriggerEnter(Collider other)
    {
        _hittedObject = other.gameObject;
        ApplyConditions(_hittedObject.tag);
    }
    GameObject _hittedObject;
    private void ApplyDamage()
    {
        _hittedObject.GetComponent<DamageSystem>().ApplyDamage(_crushMass);
    }
    private void ApplyConditions(string tag)
    {

        CollisionTagProperties colProperties = _tagCollisions.CollisionTag.SingleOrDefault(col => col.TagName == tag);
        if (colProperties !=null && colProperties.HasDamageSystem)
        {
            ApplyDamage();
        }

    }
}
