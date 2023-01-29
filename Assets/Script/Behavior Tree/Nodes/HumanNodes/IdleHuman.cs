using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class IdleHuman : Node
{
    private NavMeshAgent _navMeshAgent;
    private IAI _enemyAI;


    public IdleHuman(NavMeshAgent navMeshAgent, IAI enemyAI)
    {
        _navMeshAgent = navMeshAgent;
        _enemyAI = enemyAI;
 

    }

    public override NodeState Evaluate()
    {

        _navMeshAgent.isStopped = true;
        _enemyAI.SetIdle();
        return NodeState.RUNNING;

    }
}
