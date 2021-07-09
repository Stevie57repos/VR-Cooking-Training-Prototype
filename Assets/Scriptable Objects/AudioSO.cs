using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Misc/AudioSO")]
public class AudioSO : ScriptableObject
{
    public List<AudioClip> BGClipList = new List<AudioClip>();
}
