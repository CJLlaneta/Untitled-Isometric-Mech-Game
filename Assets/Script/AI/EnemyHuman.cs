using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.AI;
public class EnemyHuman : MonoBehaviour,IAI
{
    // Start is called before the first frame update
    [SerializeField] float _reloadDuration;
    private bool _isOnReload =false;
    private bool _isIdle = false;
    [SerializeField] private float _chaseRange;
    [SerializeField] private float _shootingRange;
    [SerializeField] private NavMeshAgent _navmeshAgent;
    [SerializeField] Node _topNode;
    [SerializeField] private Transform _target;
    [SerializeField] private Animator _animator;
    [SerializeField] List<Cover> _covers = new List<Cover>();
 
    private Transform _bestConverSpot;

   

    void Start()
    {
        ConstructAITree();
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _shootingRange);
    }
    private void ConstructAITree()
    {
        IsCoverAvailableNode coverAvailableNode = new IsCoverAvailableNode(_covers, _target, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(_navmeshAgent, this);
        OnReloadNode reloadNode = new OnReloadNode(this);
        IdleNode idleNode = new IdleNode(_navmeshAgent, this, _target);
        IsOnIdleNode isOnIdleNode = new IsOnIdleNode(_navmeshAgent, this);
        IsCoveredNode isCoveredNode = new IsCoveredNode(_target, transform);
        ChaseNode chaseNode = new ChaseNode(_target, _navmeshAgent, this);
        RangeNode chasingRangeNode = new RangeNode(_chaseRange, _target, transform);

        RangeNode shootingRangeNode = new RangeNode(_shootingRange, _target, transform);
        ShootNode shootNode = new ShootNode(_navmeshAgent, this, _target);

        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });


        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvailableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence reloadAndCoverSequence = new Sequence(new List<Node> { reloadNode, tryToCoverSelector });
        Selector reloadOrIdleSelector = new Selector(new List<Node> { reloadAndCoverSequence, idleNode });
        Sequence IdleSequence = new Sequence(new List<Node> { isOnIdleNode, reloadOrIdleSelector });
        //_topNode = new Selector(new List<Node> { reloadAndCoverSequence, shootSequence,  chaseSequence,   });
        _topNode = new Selector(new List<Node> { reloadAndCoverSequence, shootSequence, chaseSequence, });
    }
    public Transform GetTheBestCover() 
    {
        return _bestConverSpot;
    }
    public void SetBestCoverSpot(Transform cover) 
    {
        _bestConverSpot = cover;
    }

    public void Shoot() 
    {
        if (!_isOnReload)
        {
            _isOnReload = true;
            AnimationManager.Instance.PlayClip(_animator, "Shoot");
        }
        //if (!IsAnimationPlaying("Shoot"))
        //{
        //    _isOnReload = true;
        //    SetToIdle(true);
        //    AnimationManager.Instance.PlayClip(_animator, "Shoot");
        //    StartCoroutine(CheckIfDoneShooting());
        //}

    }
    IEnumerator CheckIfDoneShooting() 
    {
        do
        {
            yield return null;
        } while (IsAnimationPlaying("Shooting"));
        SetToIdle(false);
        

    }
    private bool IsAnimationPlaying(string animationClip) 
    {
        return AnimationManager.Instance.IsAnimationClipPlaying(_animator, animationClip);
    }
    public bool GetIdleStatus() 
    {
        return _isIdle;
    }
    public void SetToIdle(bool status) 
    {
        _isIdle = status;
    }
    public bool GetReloadState() 
    {
        return _isOnReload;
    }


    float _cntReload = 0;
    private void ReloadCoolDown() 
    {
        if (_isOnReload) 
        {
            _cntReload += 1 * Time.deltaTime;
            if (_cntReload >= _reloadDuration) 
            {
                _cntReload = 0;
                _isOnReload = false;
            }
        }
    }
    private void NodeEvaluation() 
    {
        //Debug.Log(_topNode.nodeState.);
        _topNode.Evaluate();
        if (_topNode.nodeState == NodeState.FAILURE) 
        {
        
        }
    }
    void Update()
    {
        NodeEvaluation();
        ReloadCoolDown();
    }
}
