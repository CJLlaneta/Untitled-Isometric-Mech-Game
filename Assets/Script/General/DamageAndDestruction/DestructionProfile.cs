using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable_Objects/General/DestructionProfile")]
public class DestructionProfile : ScriptableObject
{
    [SerializeField] List<GameObject> _destroyedPrefabs = new List<GameObject>();
    public List<GameObject> DestroyedPrefabs { get { return _destroyedPrefabs; } set { _destroyedPrefabs = value; } }

    [SerializeField] List<GameObject> _destroyedEffects  = new List<GameObject>();
    public List<GameObject> DestroyedEffects { get { return _destroyedEffects; } set { _destroyedEffects = value; } }
}
