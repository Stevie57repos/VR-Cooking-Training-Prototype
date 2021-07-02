using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BreadSpawner : ToppingsSpawner
{
    public GameObject BreadBasePrefab;
    public SandwhichHandler breadBase = null;
    protected override void CreateAndSelectTopping(SelectEnterEventArgs args)
    {
        if(breadBase == null)
        {
            CreateAndSelectBreadBase(args);
        }
        else
            base.CreateAndSelectTopping(args);
    }

    private void CreateAndSelectBreadBase(SelectEnterEventArgs args)
    {
        XRGrabInteractable breadBase = CreateAndSelectBreadBase(args.interactor.transform);
        interactionManager.ForceSelect(args.interactor, breadBase);
    }

    private XRGrabInteractable CreateAndSelectBreadBase(Transform orientation)
    {
        GameObject breadBaseObject = Instantiate(BreadBasePrefab, orientation.position, orientation.rotation);
        breadBase = breadBaseObject.GetComponent<SandwhichHandler>();
        return breadBaseObject.GetComponent<XRGrabInteractable>();
    }
}
