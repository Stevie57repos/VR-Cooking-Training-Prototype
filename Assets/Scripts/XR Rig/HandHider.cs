using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using System;

public class HandHider : MonoBehaviour
{
    public GameObject handObject = null;
    private XRDirectInteractor interactor = null;

    private void Awake()
    {
        interactor = GetComponentInChildren<XRDirectInteractor>();
    }

    private void OnEnable()
    {
        interactor.selectEntered.AddListener(Hide);
        interactor.selectExited.AddListener(Show);
    }

    private void Hide(SelectEnterEventArgs arg0)
    {
        handObject.SetActive(false);
    }
    private void Show(SelectExitEventArgs arg0)
    {
        handObject.SetActive(true);
    }

    private void OnDisable()
    {
        interactor.selectEntered.RemoveListener(Hide);
        interactor.selectExited.RemoveListener(Show);
    }
}
