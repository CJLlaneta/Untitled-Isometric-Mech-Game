using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEngageNode : Node
{
    private float _range;
    private Transform _target;
    private Transform _origin;
    private IAI _enemyAI;

    public RangeEngageNode(float range, Transform target, Transform origin, IAI enemyAI)
    {
        _range = range;
        _target = target;
        _origin = origin;
        _enemyAI = enemyAI;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(_target.position, _origin.position);
        if (distance <= _range) 
        {
            _enemyAI.SetEngageMode(true);
        }

        return  NodeState.RUNNING;
    }
}
