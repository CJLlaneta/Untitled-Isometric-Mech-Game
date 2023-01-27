using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] DamageSystem _damageSystem;
    [SerializeField] DestructionProfile _destructionProfile;
    [SerializeField] List<GameObject> _objectToDestory;
    [SerializeField] Vector3 _damageRespawnOffSet = Vector3.zero;
    void Start()
    {
        _damageSystem.OnBreakEvent += OnBreak;
    }

    private void ShowDestructionProps(List<GameObject> props)
    {
        GameObject _prop = Instantiate(props[Random.Range(0, props.Count)], transform.position, Quaternion.identity);
        Vector3 _v = _prop.transform.position;
        _v.x += _damageRespawnOffSet.x;
        _v.y += _damageRespawnOffSet.y;
        _v.z += _damageRespawnOffSet.z;
        _prop.transform.position = _v;
        _prop.SetActive(true);
    }

    private void HideObject() 
    {
        foreach(GameObject g in _objectToDestory) 
        {
            if (g.GetComponent<MeshDestroy>()) 
            {
                g.GetComponent<MeshDestroy>().DestroyMesh();
            }
            else 
            {
                g.SetActive(false);
            }

        }
    }

    private void OnBreak() 
    {
        ShowDestructionProps(_destructionProfile.DestroyedEffects);
        ShowDestructionProps(_destructionProfile.DestroyedPrefabs);
        HideObject();

    }
}
