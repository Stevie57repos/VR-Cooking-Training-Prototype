using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BreadBaseGrabHandler : XRGrabInteractable
{
    SandwichHandler sandwhichHandler;

    protected override void Awake()
    {
        base.Awake();
        sandwhichHandler = GetComponent<SandwichHandler>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        sandwhichHandler.PickedUp();
    }
}
