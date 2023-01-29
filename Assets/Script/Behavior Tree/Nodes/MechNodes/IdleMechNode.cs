using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class IdleMechNode : Node
{
    private NavMeshAgent _navMeshAgent;
    private IAI _enemyAI;


    private Vector3 _currentVelocity;
    private float _smoothDamp;
    public IdleMechNode(NavMeshAgent navMeshAgent, IAI enemyAI)
    {
        _navMeshAgent = navMeshAgent;
        _enemyAI = enemyAI;
 
        _smoothDamp = 1;
    }

    public override NodeState Evaluate()
    {

        _navMeshAgent.isStopped = true;
        _enemyAI.SetIdle();
        return NodeState.RUNNING;

    }
}
