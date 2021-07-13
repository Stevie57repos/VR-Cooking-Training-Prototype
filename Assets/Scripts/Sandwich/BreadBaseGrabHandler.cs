using UnityEngine.XR.Interaction.Toolkit;

public class BreadBaseGrabHandler : XRGrabInteractable
{
    private SandwichHandler _sandwhichHandler;
    protected override void Awake()
    {
        base.Awake();
        _sandwhichHandler = GetComponent<SandwichHandler>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        _sandwhichHandler.PickedUp();
    }
}
