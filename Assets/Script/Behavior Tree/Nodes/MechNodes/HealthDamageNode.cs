using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDamageNode : Node
{
    private IAI _enemyAI;

    public HealthDamageNode(IAI enemyAI)
    {
        _enemyAI = enemyAI;
    }

    public override NodeState Evaluate()
    {
        return _enemyAI.IsOnCriticalLevel() ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
