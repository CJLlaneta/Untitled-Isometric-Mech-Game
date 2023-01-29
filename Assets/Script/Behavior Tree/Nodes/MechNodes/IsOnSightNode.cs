using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOnSightNode : Node
{
    private Transform _target;
    private Transform _origin;

    public IsOnSightNode(Transform target, Transform origin)
    {
        _target = target;
        _origin = origin;
    }

    public override NodeState Evaluate()
    {
        RaycastHit hit;
        if (Physics.Raycast(_origin.position, _target.position - _origin.position, out hit)) 
        {
            if (hit.collider.transform == _target) 
            {
                return NodeState.SUCCESS;
            }
        }
        return NodeState.FAILURE;
    }
}
