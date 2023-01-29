using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FlankingNode : Node
{
    private IAI _enemyAI;
    private NavMeshAgent _navMeshAgent;
    private Transform _lastArea;
    public FlankingNode(IAI enemyAI, NavMeshAgent navmeshAgent)
    {
        _enemyAI = enemyAI;
        _navMeshAgent = navmeshAgent;
    }

    public override NodeState Evaluate()
    {
        if (_lastArea ==null) 
        {
            _lastArea = _enemyAI.GetTheBestCover();
        }
        float distance = Vector3.Distance(_lastArea.position, _navMeshAgent.transform.position);
        _enemyAI.OnMove();
        if (distance > 1f)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_lastArea.position);
            return NodeState.RUNNING;
        }
        else
        {
            _lastArea = _enemyAI.GetTheBestCover();
            return NodeState.RUNNING;
        }
    }
 }
