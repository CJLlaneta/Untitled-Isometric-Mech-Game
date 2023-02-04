using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentRemover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = null;
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
