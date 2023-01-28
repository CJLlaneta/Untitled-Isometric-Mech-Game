using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavAgentController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float _disableCount = 0;

    [SerializeField] NavMeshAgent _navMeshAgent;
    void Start()
    {
        
    }
    float _cntDisable = 0;
    private void Disable()
    {
        if (_disableCount > 0) 
        {
            _cntDisable += 1 * Time.deltaTime;
            if (_cntDisable >= _disableCount) 
            {
                _navMeshAgent.enabled = false;
                this.enabled = false;
            }
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        Disable();
    }
}
