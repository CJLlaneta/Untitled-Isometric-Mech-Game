using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class SoundController : MonoBehaviour
{
    [SerializeField] SoundProfile _soundProfile;
    [SerializeField] AudioSource _audioSource;

    private void Start()
    {
        Initialized();
    }
    public void SetSoundProfile(SoundProfile soundProfile) 
    {
        _soundProfile = soundProfile;
        Initialized();
    }
    private void OnEnable()
    {

    }
    private void Initialized()
    {

        if (_soundProfile != null)
        {
            _audioSource.loop = _soundProfile.IsLoop;
            if (_soundProfile.triggerStart)
            {
                TriggerSound();
            }
        }
    }

    private AudioClip GetClip() 
    {
        return _soundProfile.SFX[Random.Range(0, _soundProfile.SFX.Length)];
    }
    public void TriggerSound() 
    {
        if (!_soundProfile.OneShotTrigger) 
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Stop();
                _audioSource.clip = GetClip();
                _audioSource.Play();
            }
        }
        else 
        {
            _audioSource.PlayOneShot(GetClip());
        }

    }
}
