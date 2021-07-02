using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum ToppingType { Lettuce, Tomatoes, Ham, Bread, Cheese }

public class SandwhichToppingHandler : XRGrabInteractable
{
    public ToppingType myType;
    SandwhichHandler mySandwhichHandler;

    protected override void Awake()
    {
        base.Awake();
        mySandwhichHandler = GetComponent<SandwhichHandler>();
    }

    bool isHeld = false;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        isHeld = true;
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isHeld = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isHeld)
        {
            if (other.gameObject.GetComponent<SandwhichHandler>())
            {   
                SandwhichHandler otherSandwhichHandler = other.gameObject.GetComponent<SandwhichHandler>();
                otherSandwhichHandler.AddTopping(myType.ToString());
                if (!otherSandwhichHandler.isComplete)
                    Destroy(this.gameObject);
            }
        }
    }
}
