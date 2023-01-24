using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProjectileCollisionDetector : MonoBehaviour
{
    [SerializeField] TagCollision _tagCollisions;
    [SerializeField] GameObject _ownerOfProjectile;

    [SerializeField] string _hitID = "Caliber_Hit";


    public void SetTheOwner(GameObject owner)
    {
        _ownerOfProjectile = owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _ownerOfProjectile)
        {
            if (IsWithinTag(other.transform.tag))
            {
                //Do Something
                ApplyConditions();
            }
            else 
            {
                //By Default Destroy this object
                HitVFX();
                gameObject.SetActive(false);
            }
        }
    }
    GameObject _hitVFX;
    private void HitVFX()
    {
        _hitVFX = ObjectPoolingManager.Instance.SpawnFromPool(_hitID, transform.position, Quaternion.identity);
        _hitVFX.transform.LookAt(transform.position);
    }

    private void ApplyConditions()
    {
        HitVFX();
        gameObject.SetActive(false);
    }
    private bool IsWithinTag(string tag)
    {
        bool _ret =false;
       _ret = _tagCollisions.CollisionTag.Any(t => t.TagName == tag);
        return _ret;
    }
}
