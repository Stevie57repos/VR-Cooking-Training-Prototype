using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/ Int Event")]
public class IntEventChanelSO : ScriptableObject
{
    public UnityAction<int> orderCompleteEvent;
    public void RaiseEvent(int number)
    {
        if (orderCompleteEvent != null)
        {
            orderCompleteEvent.Invoke(number);
        }
        else
        {
            Debug.LogWarning("ON Game Manager Start was requested but nobody picked it up");
        }
    }
}
