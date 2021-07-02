using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using System;

public class HandHider : MonoBehaviour
{
    public GameObject handObject = null;

    private HandPhysics handPhysics = null;
    private XRDirectInteractor interactor = null;

    private void Awake()
    {
        handPhysics = handObject.GetComponent<HandPhysics>();
        interactor = GetComponentInChildren<XRDirectInteractor>();
    }

    private void OnEnable()
    {
        //interactor.onSelectEntered.AddListener(Hide);
        //interactor.onSelectExited.AddListener(Show);
        interactor.selectEntered.AddListener(Hide);
        interactor.selectExited.AddListener(Show);
    }
    //private void Show(XRBaseInteractable interactable)
    //{
    //    handPhysics.TeleportToTarget();
    //    handObject.SetActive(true);
    //}

    //private void Hide(XRBaseInteractable interactable)
    //{
    //    handObject.SetActive(false);
    //}
    private void Hide(SelectEnterEventArgs arg0)
    {
        handObject.SetActive(false);
    }
    private void Show(SelectExitEventArgs arg0)
    {
        handPhysics.TeleportToTarget();
        handObject.SetActive(true);
    }

    private void OnDisable()
    {
        interactor.selectEntered.RemoveListener(Hide);
        interactor.selectExited.RemoveListener(Show);
    }
}
