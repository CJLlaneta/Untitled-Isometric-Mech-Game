using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable_Objects/General/SoundProfile")]
public class SoundProfile : ScriptableObject
{
    [SerializeField] AudioClip[] _sfx;
    public AudioClip[] SFX { get { return _sfx; } set { _sfx = value; } }

    [SerializeField] bool _isLoop;
    public bool IsLoop { get { return _isLoop; } set { _isLoop = value; } }
    [SerializeField] bool _oneShotTrigger;
    public bool OneShotTrigger { get { return _oneShotTrigger; } set { _oneShotTrigger = value; } }
}
