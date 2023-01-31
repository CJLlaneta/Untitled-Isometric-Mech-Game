using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class IsEngageModeNode : Node
{

    private IAI _enemyAI;


    public IsEngageModeNode(IAI enemyAI)
    {
        _enemyAI = enemyAI;
    }

    public override NodeState Evaluate()
    {
       if (_enemyAI.IsInEngageMode()) 
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;

    }
}
