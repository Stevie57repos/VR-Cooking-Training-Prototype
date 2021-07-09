using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Audio Manager Event Channel")]
public class AudioEventChannelSO : ScriptableObject
{
    public UnityAction<AudioClip> AudioManagerEvent;
    public void RaiseEvent(AudioClip clip)
    {
        if (AudioManagerEvent != null)
        {
            AudioManagerEvent.Invoke(clip);
        }
        else
        {
            Debug.LogWarning("ON Game Manager Start was requested but nobody picked it up");
        }
    }
}
