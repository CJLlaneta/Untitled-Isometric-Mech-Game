using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSoundPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SoundController _soundController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground") 
        {
            _soundController.TriggerSound();
        }
    }
}
