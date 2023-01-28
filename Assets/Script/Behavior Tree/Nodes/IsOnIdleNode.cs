using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsOnIdleNode : Node
{
    private NavMeshAgent _navMeshAgent;
    private IAI _enemyAI;

    public IsOnIdleNode(NavMeshAgent navMeshAgent, IAI enemyAI)
    {
        _navMeshAgent = navMeshAgent;
        _enemyAI = enemyAI;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("IS On Idle : " + _enemyAI.GetIdleStatus());
        return _enemyAI.GetIdleStatus() ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
