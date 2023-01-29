using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class FlankCoverPosition : Node
{
    // Start is called before the first frame update
    private List<Cover> _availableCover;
    private Transform _target;
    private Transform _origin;
    private IAI _enemyAI;

    public FlankCoverPosition(List<Cover> availableCover, Transform target, IAI enemyAI, Transform origin)
    {
        _availableCover = availableCover;
        _target = target;
        _enemyAI = enemyAI;
        _origin = origin;
    }

    public override NodeState Evaluate()
    {
        Transform bestSpot = GetTheNearest();
        _enemyAI.SetBestCoverSpot(bestSpot);
  
        return bestSpot !=null ? NodeState.SUCCESS: NodeState.FAILURE;
    }
    float closestDistance = Mathf.Infinity;
    private Transform GetTheNearest() 
    {
        Transform _nearest = null;
        foreach (Cover cover in _availableCover)
        {
            if (cover != null) 
            {
                float distance = Vector3.Distance(_origin.position, cover.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _nearest = cover.transform;
                }
            }

        }

        if (_nearest != null) 
        {
            Cover cov = _nearest.GetComponent<Cover>();
            List<Transform> _pos = cov.GetCoverSpots();
            _nearest = _pos[Random.Range(0, _pos.Count)];
        }
        return _nearest;
    }
    private Transform FindBestCoverSpot() 
    {

        if (_enemyAI.GetTheBestCover() != null) 
        {
            if (CheckIfCoverValid(_enemyAI.GetTheBestCover())) 
            {
                return _enemyAI.GetTheBestCover();
            }
        }
        float minAngle = 30;
        Transform bestSpot = null;
        foreach(Cover cover in _availableCover) 
        {
            Transform bestSpotInCover = FindBestSpotInCover(cover,ref minAngle);
            if (bestSpotInCover != null) 
            {
                bestSpot = bestSpotInCover; ;
            }
        }
        return bestSpot;
    }

    private Transform FindBestSpotInCover(Cover cover,ref float minAngle) 
    {
        List<Transform> availableSpots = cover.GetCoverSpots();
        Transform bestSpot = null;

        foreach (Transform availableSpot in availableSpots) 
        {
            Vector3 _dir = _target.position - availableSpot.position;
            if (CheckIfCoverValid(availableSpot)) 
            {
                float angle = Vector3.Angle(availableSpot.forward, _dir);
                if (angle < minAngle) 
                {
                    minAngle= angle;
                    bestSpot = availableSpot;
                }
            }
        }
        return bestSpot;

    }

    private bool CheckIfCoverValid(Transform spot) 
    {
        RaycastHit hit;
        Vector3 _dir = _target.position - spot.position; 
        if (Physics.Raycast(spot.position, _dir, out hit)) 
        {
            if (hit.collider.transform != _target) 
            {
                return true;
            }
        }
        return false;
    }
}
