using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    [SerializeField] float _lifeDuration =3f;
    [SerializeField] List<GameObject> _includeThisObject = new List<GameObject>();
    void Start()
    {
        
    }
    void OnEnable()
    {
        cnt = 0;
    }
    private void ObjectToInclude() 
    {
        foreach(GameObject b in _includeThisObject) 
        {
            b.SetActive(false);
        }
    }
    float cnt = 0;
    void Update()
    {
        cnt +=1 * Time.deltaTime;
        if (cnt >=_lifeDuration)
        {
            cnt = 0;
            ObjectToInclude();
            gameObject.SetActive(false);
        }
    }
}
