using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ShootNode : Node
{
    private NavMeshAgent _navMeshAgent;
    private IAI _enemyAI;
    private Transform _target;

    private Vector3 _currentVelocity;
    private float _smoothDamp;
    public ShootNode(NavMeshAgent navMeshAgent, IAI enemyAI, Transform target)
    {
        _navMeshAgent = navMeshAgent;
        _enemyAI = enemyAI;
        _target = target;
        _smoothDamp = 1;
    }

    public override NodeState Evaluate()
    {

        _navMeshAgent.isStopped = true;
        _enemyAI.Shoot();
        Vector3 _dir = _target.position - _navMeshAgent.transform.position;
        Vector3 _currentDir = Vector3.SmoothDamp(_navMeshAgent.transform.forward, _dir, ref _currentVelocity, _smoothDamp);
        Quaternion _rot = Quaternion.LookRotation(_currentDir, Vector3.up);
        _navMeshAgent.transform.rotation = _rot;
        return NodeState.RUNNING;

    }
}
