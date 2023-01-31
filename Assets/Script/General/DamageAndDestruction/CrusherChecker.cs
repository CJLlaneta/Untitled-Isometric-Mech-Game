using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DamageSystem;

public class CrusherChecker : MonoBehaviour
{
    [SerializeField] TagCollision _tagCollisions;
    [SerializeField] float _crushMass = 100;
    [SerializeField] List<Collider> _ignoreCollider = new List<Collider>();
    private void OnTriggerEnter(Collider other)
    {
        if (IsNotInIgnore(other)) 
        {
            _hittedObject = other.gameObject;
            ApplyConditions(_hittedObject.tag);
        }

    }

    private bool IsNotInIgnore(Collider other) 
    {
        bool _ret = true;
        if (_ignoreCollider.Contains(other)) 
        {
            return false;
        }
        return _ret;
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
