using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Cover : MonoBehaviour
{
    [SerializeField] List<Transform> _coverSpots = new List<Transform>();


    public List<Transform> GetCoverSpots() 
    {
        _coverSpots.RemoveAll(item => item == null);
        return _coverSpots;
    }

}
