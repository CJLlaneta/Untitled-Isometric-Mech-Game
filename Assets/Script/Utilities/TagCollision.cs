using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable_Objects/Utilities/TagsCollision")]
public class TagCollision : ScriptableObject
{
  [SerializeField] List<CollisionTagProperties> _collisionTag = new List<CollisionTagProperties>();
  public List<CollisionTagProperties> CollisionTag{get {return _collisionTag;} private set {_collisionTag =value;}}

}

[System.Serializable]

public class CollisionTagProperties
{
    public string TagName = "Default";
    public bool PassThrough = false;
    public bool HasDamageSystem = false;
}