using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    [SerializeField] float _lifeDuration =3f;
    void Start()
    {
        
    }
    void OnEnable()
    {
        cnt = 0;
    }

    float cnt = 0;
    void Update()
    {
        cnt +=1 * Time.deltaTime;
        if (cnt >=_lifeDuration)
        {
            cnt = 0;
            gameObject.SetActive(false);
        }
    }
}
