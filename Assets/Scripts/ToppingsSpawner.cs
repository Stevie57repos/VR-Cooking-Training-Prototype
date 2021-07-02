using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToppingsSpawner : XRBaseInteractable
{
    public GameObject toppingPrefab = null;

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(CreateAndSelectTopping);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(CreateAndSelectTopping);
    }

    protected virtual void CreateAndSelectTopping(SelectEnterEventArgs args)
    {
        // Create topping, force into interacting hand
        SandwhichToppingHandler topping = CreateTopping(args.interactor.transform);
        interactionManager.ForceSelect(args.interactor, topping);
    }
    private SandwhichToppingHandler CreateTopping(Transform orientation)
    {
        GameObject toppingObject = Instantiate(toppingPrefab, orientation.position, orientation.rotation);
        return toppingObject.GetComponent<SandwhichToppingHandler>();
    }
}
