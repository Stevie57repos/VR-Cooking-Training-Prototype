using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Transform Event Channel")]
public class CorrectOrderEffect : ScriptableObject
{
    public UnityAction<Transform> OrderComplete;
    public void RaiseEvent(Transform location)
    {
        if (OrderComplete != null)
        {
            OrderComplete.Invoke(location);
        }
        else
        {
            Debug.LogWarning("ON Game Manager Start was requested but nobody picked it up");
        }
    }
}
