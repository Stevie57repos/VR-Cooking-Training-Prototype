using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BuildingGrab : XRBaseInteractable
{
    [SerializeField] private float Duration;
    [SerializeField] private bool isGrabbed = false;

    [SerializeField] private GameObject BuildingTop;
    [SerializeField] private GameObject BuildingBottom;

    [SerializeField] XRBaseInteractor interactor;

    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    private void Start()
    {
        _animator.enabled = true;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        //base.OnSelectEntered(args);
        Debug.Log($"I detected a grab");
        isGrabbed = true;
        interactor = args.interactor;
        _animator.SetBool("isGrabbed", true);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        isGrabbed = false;
        if(Duration > 0) 
            interactor = null;
        _animator.SetBool("isGrabbed", false);
    }

    private void Update()
    {
        if (isGrabbed & Duration > 0)
            Duration -= Time.deltaTime;
        
        if (Duration <= 0)
            CreateAndSelectBuildingTop();
    }

    private void CreateAndSelectBuildingTop()
    {
        BuildingBottom.SetActive(true);
        BuildingTop.SetActive(true);
        XRGrabInteractable grab = BuildingTop.GetComponent<XRGrabInteractable>();
        interactionManager.SelectExit(interactor, this.gameObject.GetComponent<BuildingGrab>());
        interactionManager.ForceSelect(interactor, grab);
        this.gameObject.SetActive(false);
    }
}
