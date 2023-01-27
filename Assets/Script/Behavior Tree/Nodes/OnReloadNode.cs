using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnReloadNode : Node
{

    private IAI _enemyAI;

    public OnReloadNode(IAI enemyHuman)
    {
        _enemyAI = enemyHuman;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log(_enemyAI.GetReloadState());
        //NodeState.FAILURE
        //return _enemyAI.GetReloadState() ? NodeState.SUCCESS: NodeState.FAILURE;
        return _enemyAI.GetReloadState() ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
