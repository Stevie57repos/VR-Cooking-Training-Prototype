using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToppingsSpawner : XRBaseInteractable
{
    public GameObject toppingPrefab = null;
    [SerializeField] AudioSource _canonAudioSource;
    [SerializeField] AudioClip clip;

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(CreateAndSelectTopping);
        selectEntered.AddListener(PlaySound);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(CreateAndSelectTopping);
        selectEntered.RemoveListener(PlaySound);
    }

    protected virtual void CreateAndSelectTopping(SelectEnterEventArgs args)
    {
        SandwhichToppingHandler topping = CreateTopping(args.interactor.transform);
        interactionManager.ForceSelect(args.interactor, topping);
        _canonAudioSource.PlayOneShot(clip);
    }

    private void PlaySound(SelectEnterEventArgs args)
    {
        _canonAudioSource.PlayOneShot(clip);
    }

    private SandwhichToppingHandler CreateTopping(Transform orientation)
    {
        GameObject toppingObject = Instantiate(toppingPrefab, orientation.position, orientation.rotation);
        return toppingObject.GetComponent<SandwhichToppingHandler>();
    }
}
