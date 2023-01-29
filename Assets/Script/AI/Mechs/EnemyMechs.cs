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
    [SerializeField] Transform _sightLocation;
    [SerializeField] GameObject _parentOwner;
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
        _bodyController.SetTheWeaponOwner(_parentOwner);
        GetTheMapCover();
        ConstructBrainNode();
    }

    private void ConstructBrainNode() 
    {
        RangeNode _alertRange = new RangeNode(_chaseRange, _target, transform);
        Inverter alertNode = new Inverter(_alertRange);
        IdleMechNode idleNode = new IdleMechNode(_navmeshAgent, this);
        ChaseNode chaseNode = new ChaseNode(_target, _navmeshAgent, this);
        RangeAimingNode chasingRangeNode = new RangeAimingNode(_chaseRange, _target, transform,this);
        RangeAimingNode shootingRangeNode = new RangeAimingNode(_shootingRange, _target, transform, this);
        ShootOnRunMechNode shootMoveNode = new ShootOnRunMechNode(_navmeshAgent, this);
        ShootMechNode shootHoldNode = new ShootMechNode(_navmeshAgent, this,_target);
        IsOnSightNode isOnSightNode = new IsOnSightNode(_target, _sightLocation);

        Sequence SeekAndFire = new Sequence(new List<Node> { isOnSightNode, shootingRangeNode });
        Sequence neutralSequence = new Sequence(new List<Node> { alertNode, idleNode });
        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new Sequence(new List<Node> { SeekAndFire, shootHoldNode });


        _brainNode = new Selector(new List<Node> { neutralSequence, shootSequence, chaseSequence, });
    }
    public void ShutDownTheMech() 
    {
        _bodyController.ShutDown();

        _navmeshAgent.enabled = false;
        this.enabled = false;
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
        float _per = _criticalLevelPercentage / 100;
        float _criValue = GetMaxHP() * _per;
       // Debug.Log("HP: " + GetHealth() + " crit:" + _criValue);
        if (_currentHP <= _criValue) 
        {
            return true;
        }
        return _ret;
    }
    private float GetMaxHP() 
    {
        return _damageController.GetMaxHealth();
    }
    public float GetHealth() 
    {
        return _damageController.GetCurrentHealth();
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
    public void SetBestCoverSpot(Transform bestPosition)
    {
        _bestCover = bestPosition;
    }
}
