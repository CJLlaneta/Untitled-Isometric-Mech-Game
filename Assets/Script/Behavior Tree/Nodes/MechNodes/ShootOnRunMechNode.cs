using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ShootOnRunMechNode : Node
{
    private NavMeshAgent _navMeshAgent;
    private IAI _enemyAI;



    public ShootOnRunMechNode(NavMeshAgent navMeshAgent, IAI enemyAI)
    {
        _navMeshAgent = navMeshAgent;
        _enemyAI = enemyAI;

    }

    public override NodeState Evaluate()
    {
        _enemyAI.Shoot();
        _enemyAI.OnAim();
        return NodeState.SUCCESS;
    }
}
