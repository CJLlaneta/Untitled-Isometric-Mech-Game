using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAimingNode : Node
{
    private float _range;
    private Transform _target;
    private Transform _origin;
    private IAI _mechAI;

    public RangeAimingNode(float range, Transform target, Transform origin, IAI mechAI)
    {
        _range = range;
        _target = target;
        _origin = origin;
        _mechAI = mechAI;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(_target.position, _origin.position);
        _mechAI.OnAim();
        return distance <= _range ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
