using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ShootMechNode : Node
{
    private NavMeshAgent _navMeshAgent;
    private IAI _enemyAI;
    private Transform _target;

    private Vector3 _currentVelocity;
    private float _smoothDamp;
    public ShootMechNode(NavMeshAgent navMeshAgent, IAI enemyAI, Transform target)
    {
        _navMeshAgent = navMeshAgent;
        _enemyAI = enemyAI;
        _target = target;
        _smoothDamp = 1;
    }

    public override NodeState Evaluate()
    {
        _navMeshAgent.isStopped = true;
        _enemyAI.SetIdle();
        _enemyAI.Shoot();
        _enemyAI.OnAim();
        return NodeState.RUNNING;
    }
}
