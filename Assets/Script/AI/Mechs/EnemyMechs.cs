using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMechs : MonoBehaviour,IAI
{
    [SerializeField] DamageControllerMech _damageController;
    [SerializeField] float _criticalLevelPercentage = 60;
    [SerializeField] float _brainInverval = 1f;
    [SerializeField] float _shootInterval = 1;
    [SerializeField] BodyController _bodyController;
    [SerializeField] Node _brainNode;
    [SerializeField] Transform _target;
    [SerializeField] NavMeshAgent _navmeshAgent;
    [SerializeField] List<Cover> _covers = new List<Cover>();
    [SerializeField] private float _chaseRange;
    [SerializeField] private float _shootingRange;
    private Transform _bestCover;
    Vector3 _point = Vector3.zero;

    void Start()
    {
        Initialized();
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _shootingRange);
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
        _navmeshAgent.speed = _bodyController.MovementSpeed;
        _bodyController.SetTheWeaponOwner(gameObject);
        GetTheMapCover();
        ConstructBrainNode();
    }

    private void ConstructBrainNode() 
    {
        IsCoverAvailableNode coverAvailableNode = new IsCoverAvailableNode(_covers, _target, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(_navmeshAgent, this);
        RangeNode _alertRange = new RangeNode(_chaseRange, _target, transform);
        Inverter alertNode = new Inverter(_alertRange);
        IdleMechNode idleNode = new IdleMechNode(_navmeshAgent, this);
        HealthDamageNode HealthDamageNode = new HealthDamageNode(this);
        IsCoveredNode isCoveredNode = new IsCoveredNode(_target, transform, this);
        ChaseNode chaseNode = new ChaseNode(_target, _navmeshAgent, this);
        RangeAimingNode chasingRangeNode = new RangeAimingNode(_chaseRange, _target, transform,this);
        RangeAimingNode shootingRangeNode = new RangeAimingNode(_shootingRange, _target, transform, this);
        ShootMechNode shootNode = new ShootMechNode(_navmeshAgent, this, _target);

        Sequence neutralSequence = new Sequence(new List<Node> { alertNode, idleNode });
        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });


        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvailableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence criticalAndCoverSequence = new Sequence(new List<Node> { HealthDamageNode, tryToCoverSelector });
        //neutralSequence,  criticalAndCoverSequence, shootSequence,
        _brainNode = new Selector(new List<Node> { criticalAndCoverSequence, shootSequence, chaseSequence, });
    }

    float _cntBrainInterval = 0;
    private void EvaluatateBrain() 
    {
        _cntBrainInterval += 1 * Time.deltaTime;
        if (_cntBrainInterval >= _brainInverval)
        {
            _cntBrainInterval = 0;
            _brainNode.Evaluate();
        }
        _brainNode.Evaluate();

    }
    void Update()
    {
        EvaluatateBrain();
    }


    public bool GetReloadState() 
    {
        return false;
    }
    public bool IsOnCriticalLevel()
    {
        bool _ret = false;
        float _currentHP = GetHealth();
        float _per = _criticalLevelPercentage / _currentHP;
        float _criValue = _currentHP * _per;
        if (GetHealth() <= _criValue) 
        {
            return true;
        }
        return _ret;
    }

    public float GetHealth() 
    {
        return _damageController.GetTotalHealth();
    }
    float _cntShootInterval = 0;
    public void Shoot() 
    {
        _cntShootInterval += 1 * Time.deltaTime;
        if (_cntShootInterval >= _shootInterval)
        {
            _cntShootInterval = 0;
            _bodyController.FiringWeapon(WeaponHandler.WeaponLocation.right, _target.position);
            _bodyController.FiringWeapon(WeaponHandler.WeaponLocation.left, _target.position);
        }
    }
    public void SetIdle() 
    {
        _bodyController.IsMoving(false);
    }
    public void OnMove() 
    {
        Quaternion newRotation = Quaternion.LookRotation(_navmeshAgent.destination, Vector3.up);
        //_bodyController.ChasisRotation(newRotation);
        if (_navmeshAgent.speed <= 0) 
        {
            _navmeshAgent.speed = _bodyController.MovementSpeed;
        }
        _bodyController.IsMoving(true);
    }
   
    public void OnAim()
    {
        _point = _target.position;
        Vector3 heightCorrectedPoint = new Vector3(_point.x, transform.position.y, _point.z);
        Vector3 relativePos = heightCorrectedPoint - _navmeshAgent.transform.position;
        Quaternion newRotation = Quaternion.LookRotation(relativePos);
        _bodyController.UpperRotation(newRotation);
    }
    public Transform GetTheBestCover() 
    {
        return _bestCover;
    }
    public void SetBestCoverSpot(Transform cover)
    {
        _bestCover = cover;
    }
}
