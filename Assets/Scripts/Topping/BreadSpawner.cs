using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BreadSpawner : ToppingsSpawner
{
    public GameObject BreadBasePrefab;
    public SandwichHandler breadBase ;
    [SerializeField] private List<GameObject> _BreadBaseList = new List<GameObject>();
    protected override void Awake()
    {
        base.Awake();
        CreateBreadBasePool();
    }
    private void CreateBreadBasePool()
    {
        for(int i = 0; i < _objectPoolAmount; i ++)
        {
            GameObject breadBaseGO = Instantiate(BreadBasePrefab);
            breadBaseGO.SetActive(false);
            _BreadBaseList.Add(breadBaseGO);
        }
    }
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
        GameObject breadBaseGO = RetrieveBreadBase(args.interactor.transform);
        breadBase = breadBaseGO.GetComponent<SandwichHandler>();
        breadBase.BreadSpawner = this;    
        XRGrabInteractable breadBaseGrab = breadBaseGO.GetComponent<XRGrabInteractable>();
        interactionManager.ForceSelect(args.interactor, breadBaseGrab);
    }
    private GameObject RetrieveBreadBase(Transform orientation)
    {
        for (int i = 0; i < _BreadBaseList.Count; i++)
        {
            if (!_BreadBaseList[i].activeInHierarchy)
            {
                _BreadBaseList[i].transform.position = orientation.position;
                _BreadBaseList[i].transform.rotation = orientation.rotation;
                _BreadBaseList[i].SetActive(true);
                return _BreadBaseList[i];
            }
            else
                Debug.Log($"Nothing left in the pool");
        }
        return null;
    }
}
