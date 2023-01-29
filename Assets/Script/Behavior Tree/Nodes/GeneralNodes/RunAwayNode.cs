using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RunAwayNode : Node
{
    private NavMeshAgent _navmeshAgent;
    private Transform _target;
    private IAI _enemyAI;
    private float _maximumFleeDistance;

    public RunAwayNode(NavMeshAgent navmeshAgent, Transform target, float maximumFleeDistance, IAI enemyAI)
    {
        _navmeshAgent = navmeshAgent;
        _target = target;
        _maximumFleeDistance = maximumFleeDistance;
        _enemyAI = enemyAI;
    }
    Vector3 fleeDirection = Vector3.zero;
    Vector3 _lastDirection = Vector3.zero;
    private Vector3 SetRandomDestination() 
    {
        Vector3 _flee = _navmeshAgent.transform.position - _target.position + Random.onUnitSphere;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(_navmeshAgent.transform.position + _flee, out hit, _maximumFleeDistance, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return _navmeshAgent.transform.position - _target.position;
    }
    public override NodeState Evaluate() 
    {
        
        if (fleeDirection == Vector3.zero) 
        {
            fleeDirection = SetRandomDestination();
        }
        else if (Vector3.Distance(fleeDirection, _navmeshAgent.transform.position) <= 2) 
        {
            fleeDirection = SetRandomDestination();
        }

        _enemyAI.OnMove();
        _navmeshAgent.isStopped = false;
        //if (_lastDirection != fleeDirection) 
        //{
        //    _navmeshAgent.SetDestination(fleeDirection);
        //    _lastDirection = fleeDirection;
        //}
        _navmeshAgent.SetDestination(fleeDirection);
        return NodeState.RUNNING;
    }
}
