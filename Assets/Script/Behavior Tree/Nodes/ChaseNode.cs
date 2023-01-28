using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ChaseNode : Node
{
    private Transform _target;
    private NavMeshAgent _navMeshAgent;
    private IAI _enemyAI;

    public ChaseNode(Transform target, NavMeshAgent navMeshAgent, IAI iAI)
    {
        _target = target;
        _navMeshAgent = navMeshAgent;
        _enemyAI = iAI;
    }

    public override NodeState Evaluate()
    {

        float distance = Vector3.Distance(_target.position, _navMeshAgent.transform.position);
        if (distance > 2f) 
        {

            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_target.position);
            return NodeState.RUNNING;
        }
        else 
        {

            _navMeshAgent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }
}
