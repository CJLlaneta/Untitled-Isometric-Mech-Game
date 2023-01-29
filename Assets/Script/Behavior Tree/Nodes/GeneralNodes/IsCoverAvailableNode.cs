using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCoverAvailableNode : Node
{
    // Start is called before the first frame update
    private List<Cover> _availableCover;
    private Transform _target;
    private IAI _enemyAI;

    public IsCoverAvailableNode(List<Cover> availableCover, Transform target, IAI enemyAI)
    {
        _availableCover = availableCover;
        _target = target;
        _enemyAI = enemyAI;
    }

    public override NodeState Evaluate()
    {
        Transform bestSpot = FindBestCoverSpot();
        _enemyAI.SetBestCoverSpot(bestSpot);
  
        return bestSpot !=null ? NodeState.SUCCESS: NodeState.FAILURE;
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
        float minAngle = 90;
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
