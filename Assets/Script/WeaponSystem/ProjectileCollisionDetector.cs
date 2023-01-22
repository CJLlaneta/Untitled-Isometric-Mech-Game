using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProjectileCollisionDetector : MonoBehaviour
{
    [SerializeField] TagCollision _tagCollisions;
    [SerializeField] GameObject _ownerOfProjectile;


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
                gameObject.SetActive(false);
            }
            else 
            {
                //By Default Destroy this object
                gameObject.SetActive(false);
            }
        }
    }

    private bool IsWithinTag(string tag)
    {
        bool _ret =false;
       _ret = _tagCollisions.CollisionTag.Any(t => t.TagName == tag);
        return _ret;
    }
}
