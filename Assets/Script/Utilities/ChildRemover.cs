using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildRemover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            child.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
