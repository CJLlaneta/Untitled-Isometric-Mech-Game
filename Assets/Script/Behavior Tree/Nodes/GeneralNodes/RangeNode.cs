using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : Node
{
    private float _range;
    private Transform _target;
    private Transform _origin;

    public RangeNode(float range, Transform target, Transform origin)
    {
        _range = range;
        _target = target;
        _origin = origin;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(_target.position, _origin.position);

        return distance <= _range ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
