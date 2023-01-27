using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    [SerializeField] List<Transform> _coverSpots = new List<Transform>();


    public List<Transform> GetCoverSpots() 
    {
        return _coverSpots;
    }
}
