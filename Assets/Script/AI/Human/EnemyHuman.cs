using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.AI;
public class EnemyHuman : MonoBehaviour,IAI
{
    // Start is called before the first frame update
    [SerializeField] float _reloadDuration;
    [SerializeField] WeaponHandler _weaponHandler;
    [SerializeField] Transform _muzzlePoint;
    private bool _isOnReload =false;
    [SerializeField] private float _chaseRange;
    [SerializeField] private float _shootingRange;
    [SerializeField] private float _movespeed;
    [SerializeField] private NavMeshAgent _navmeshAgent;
    [SerializeField] Node _topNode;
    [SerializeField] private Transform _target;
    [SerializeField] private Animator _animator;
    [SerializeField] List<Cover> _covers = new List<Cover>();
    private bool _isEngage = false;
    private Transform _bestConverSpot;

   

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
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _shootingRange);
    }
    private void ConstructAITree()
    {
        IsCoverAvailableNode coverAvailableNode = new IsCoverAvailableNode(_covers, _target, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(_navmeshAgent, this);
        OnReloadNode reloadNode = new OnReloadNode(this);

        IsCoveredNode isCoveredNode = new IsCoveredNode(_target, transform,this);
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

    public bool IsOnCriticalLevel() 
    {
        return false;
    }
    public float GetHealth()
    {
        return 1;
    }

   public void SetEngageMode(bool status)
    {
        _isEngage = status;
    }
    public bool IsInEngageMode()
    {
        return _isEngage;
    }
    public void Shoot() 
    {
        if (!_isOnReload)
        {
            // Debug.Log("fire");
            _muzzlePoint.LookAt(_target.position);
            _weaponHandler.OnShoot(_target.position);
             _isOnReload = true;
            AnimationManager.Instance.PlayClip(_animator, "Shoot");
        }

    }

    private bool IsAnimationPlaying(string animationClip) 
    {
        return AnimationManager.Instance.IsAnimationClipPlaying(_animator, animationClip);
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

    float _cntExecute = 0;
    void Update()
    {
        _cntExecute += 1 * Time.deltaTime;
        if (_cntExecute >= 1) 
        {
            _cntExecute = 0;
            NodeEvaluation();
        }
        ReloadCoolDown();
    }
}
