using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum ToppingType { Lettuce, Tomatoes, Ham, Bread, Cheese }

public class SandwichToppingHandler : XRGrabInteractable
{
    public ToppingType MyType;
    protected override void Awake()
    {
        base.Awake();
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
            if (other.gameObject.GetComponent<SandwichHandler>())
            {   
                SandwichHandler otherSandwhichHandler = other.gameObject.GetComponent<SandwichHandler>();
                otherSandwhichHandler.AddTopping(MyType.ToString());
                this.gameObject.SetActive(false);
            }
        }
    }
}
