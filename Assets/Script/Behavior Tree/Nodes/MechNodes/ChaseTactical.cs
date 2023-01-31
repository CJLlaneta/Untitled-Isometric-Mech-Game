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
    private Vector3 SetRandomDestination() 
    {
        //Vector3 _flee = _navmeshAgent.transform.position - _target.position + Random.onUnitSphere;
        //NavMeshHit hit;
        //if (NavMesh.SamplePosition(_navmeshAgent.transform.position + _flee, out hit, _maximumDistance, NavMesh.AllAreas))
        //{
        //    return hit.position;
        //}
        //return _navmeshAgent.transform.position - _target.position;
        Vector3 randomLocation = _target.transform.position + Random.onUnitSphere * _maximumDistance;
        randomLocation.y = _target.transform.position.y;
        do
        {
            randomLocation = _target.transform.position + Random.onUnitSphere * _maximumDistance;
            randomLocation.y = _target.transform.position.y;
        } while (!IsPositionReachable(randomLocation));
        //return _navmeshAgent.transform.position - _target.position;
        return randomLocation;
    }
    public override NodeState Evaluate() 
    {
        
        if (engageDirection == Vector3.zero) 
        {
            engageDirection = SetRandomDestination();
        }
        else if (Vector3.Distance(engageDirection, _navmeshAgent.transform.position) <= (_maximumDistance/2)) 
        {
            engageDirection = SetRandomDestination();
        }
        _enemyAI.OnAim();
        _enemyAI.OnMove();
        _navmeshAgent.isStopped = false;
        _navmeshAgent.SetDestination(engageDirection);
        return NodeState.RUNNING;
    }
}
