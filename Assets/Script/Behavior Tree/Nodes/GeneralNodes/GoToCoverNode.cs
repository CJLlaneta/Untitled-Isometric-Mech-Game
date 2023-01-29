using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GoToCoverNode : Node
{
   
    private NavMeshAgent _navMeshAgent;
    private IAI _enemyAI;

    public GoToCoverNode( NavMeshAgent navMeshAgent, IAI iAI)
    {
        
        _navMeshAgent = navMeshAgent;
        _enemyAI = iAI;
    }

    public override NodeState Evaluate()
    {
        Transform cover = _enemyAI.GetTheBestCover();
     
        if (cover == null) 
        {
            return NodeState.FAILURE;
        }
        float distance = Vector3.Distance(cover.position, _navMeshAgent.transform.position);

        if (distance > 0.2f) 
        {
            //Debug.Log(cover.name + " is the best cover");
            _enemyAI.OnMove();
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(cover.position);
            return NodeState.RUNNING;
        }
        else 
        {
            _enemyAI.SetIdle();
            _navMeshAgent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }
}
