using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;



public class ChaseTactical : Node
{
    private NavMeshAgent _navmeshAgent;
    private Transform _target;
    private IAI _enemyAI;
    private float _maximumDistance;

    public ChaseTactical(NavMeshAgent navmeshAgent, Transform target, float maximumDistance, IAI enemyAI)
    {
        _navmeshAgent = navmeshAgent;
        _target = target;
        _maximumDistance = maximumDistance;
        _enemyAI = enemyAI;
    }
    Vector3 engageDirection = Vector3.zero;
    bool IsPositionReachable(Vector3 position)
    {
        NavMesh.SamplePosition(position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas);
        return hit.hit;
    }
    Vector3 randomLocation = Vector3.zero;
    NavMeshHit _navhit;
    Vector3 targetPosition;
    private Vector3 SetRandomDestination() 
    {
        //do
        //{
        //    randomLocation = _target.position + Random.onUnitSphere * _maximumDistance;
        //    randomLocation.y = _target.transform.position.y;
        //} while (!IsPositionReachable(randomLocation));
        //randomLocation = Random.onUnitSphere *Random.Range(_maximumDistance * 0.5f, _maximumDistance);
        //if (NavMesh.SamplePosition(_target.position + randomLocation, out _navhit, 5f, NavMesh.AllAreas))
        //{
        //    return _navhit.position;
        //}
        //return _target.position + Random.onUnitSphere * _maximumDistance;

        float randomDistance = Random.Range(_maximumDistance * 0.5f, _maximumDistance);
        randomLocation = Random.onUnitSphere;
        targetPosition = _target.position + randomLocation * randomDistance;
        if (NavMesh.SamplePosition(targetPosition, out _navhit, randomDistance, NavMesh.AllAreas))
        {
            return _navhit.position;
        }
        return _target.position + Random.onUnitSphere * _maximumDistance;
    }
    float _cntCheck = 0;
    public override NodeState Evaluate() 
    {
        _cntCheck += 1 * Time.deltaTime;
        if (engageDirection == Vector3.zero) 
        {
            engageDirection = SetRandomDestination();
        }
        else if (Vector3.Distance(_navmeshAgent.transform.position,engageDirection) <= 2)
        {
            engageDirection = SetRandomDestination();
            _cntCheck = 0;
        }
        else if (_cntCheck >=3) 
        {
            engageDirection = SetRandomDestination();
            _cntCheck = 0;
        }
        _enemyAI.OnAim();
        _enemyAI.OnMove();
        _navmeshAgent.isStopped = false;
        _navmeshAgent.SetDestination(engageDirection);
        return NodeState.RUNNING;
    }
}
