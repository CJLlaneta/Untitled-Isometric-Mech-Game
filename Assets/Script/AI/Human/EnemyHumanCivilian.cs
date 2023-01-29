using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.AI;
public class EnemyHumanCivilian : MonoBehaviour,IAI
{
    // Start is called before the first frame update
    [SerializeField] float _evacuateRange = 5;
    [SerializeField] float _evaluateInterval = 1;
    [SerializeField] private float _movespeed;
    [SerializeField] private NavMeshAgent _navmeshAgent;
    [SerializeField] Node _topNode;
    [SerializeField] private Transform _target;
    [SerializeField] private Animator _animator;
    [SerializeField] List<Cover> _covers = new List<Cover>();
 
    private Transform _bestConverSpot;
    private bool _isOnReload = false;


    void Start()
    {
        Initialized();
    }
    private void GetTheMapCover() 
    {
        GameObject[] cov = GameObject.FindGameObjectsWithTag("CoverSystem");
        _covers.Clear();
        foreach (GameObject g in cov) 
        {
            _covers.Add(g.GetComponent<Cover>());
        }
    }
    private void Initialized() 
    {
        GetTheMapCover();
        _navmeshAgent.speed = _movespeed;
        ConstructAITree();
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _evacuateRange);
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, _shootingRange);
    }
    private void ConstructAITree()
    {
        RunAwayNode _runAwayNode = new RunAwayNode(_navmeshAgent, _target, _evacuateRange, this);
        IdleHuman idleHuman = new IdleHuman(_navmeshAgent, this);
        RangeNode isinRangeNode = new RangeNode(_evacuateRange, _target, transform);
        Sequence evacuateSequence = new Sequence(new List<Node> { isinRangeNode, _runAwayNode });

        _topNode = new Selector(new List<Node> { evacuateSequence, idleHuman });
    }
    public Transform GetTheBestCover() 
    {
        return _bestConverSpot;
    }
    public void SetBestCoverSpot(Transform cover) 
    {
        _bestConverSpot = cover;
    }

    public bool IsOnCriticalLevel() 
    {
        return false;
    }
    public float GetHealth()
    {
        return 1;
    }
    public void Shoot() 
    {


    }


    public void SetIdle() 
    {
        AnimationManager.Instance.SetAnimationBoolean(_animator, "Running", false);
    }

    public void OnMove()
    {
        AnimationManager.Instance.SetAnimationBoolean(_animator, "Running", true);
    }
    public void OnAim()
    {

    }

    public bool GetReloadState() 
    {
        return _isOnReload;
    }


    private void NodeEvaluation() 
    {
        //Debug.Log(_topNode.nodeState.);
        _topNode.Evaluate();

    }

    float _cntExecute = 0;
    void Update()
    {
        _cntExecute += 1 * Time.deltaTime;
        if (_cntExecute >= _evaluateInterval) 
        {
            _cntExecute = 0;
            NodeEvaluation();
        }
    }
}
