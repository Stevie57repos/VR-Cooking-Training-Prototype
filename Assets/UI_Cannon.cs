using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Cannon : MonoBehaviour
{
    public GameManagerEventChannelSO FireCanonEvent;

    public void FireCanon()
    {
        FireCanonEvent.RaiseEvent();
    } 
}
