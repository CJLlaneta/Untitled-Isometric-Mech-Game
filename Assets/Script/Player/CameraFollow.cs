using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera _cam;

    [SerializeField] Transform _player;
    [SerializeField] float _threshold;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePos);
        Vector3 targetPos = (_player.position + mousePos)/2;

        targetPos.x = Mathf.Clamp(targetPos.x, -_threshold + _player.position.x, _threshold + _player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -_threshold + _player.position.y, _threshold + _player.position.y);

        transform.position = targetPos;
    }
}
