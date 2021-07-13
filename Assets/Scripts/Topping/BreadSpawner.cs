using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BreadSpawner : ToppingsSpawner
{
    public GameObject BreadBasePrefab;
    public SandwichHandler breadBase ;
    [SerializeField] private List<GameObject> _BreadBasePool = new List<GameObject>();

    protected override void OnEnable()
    {
        base.OnEnable();
        _levelLoaded.GameManagerEvent += CreateBreadBasePool;
        _EndLevelLoaded.GameManagerEvent += ClearBreadBasePool;
    }

    protected override void OnDisable()
    {
        _levelLoaded.GameManagerEvent -= CreateBreadBasePool;
        _EndLevelLoaded.GameManagerEvent -= ClearBreadBasePool;
        base.OnDisable();
    }
    private void ClearBreadBasePool()
    {
        _BreadBasePool.Clear();
    }
    protected override void Awake()
    {
        base.Awake();
        //CreateBreadBasePool();     
    }
    private void CreateBreadBasePool()
    {
        for(int i = 0; i < _objectPoolAmount; i ++)
        {
            GameObject breadBaseGO = Instantiate(BreadBasePrefab);
            breadBaseGO.SetActive(false);
            _BreadBasePool.Add(breadBaseGO);
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
        for (int i = 0; i < _BreadBasePool.Count; i++)
        {
            if (!_BreadBasePool[i].activeInHierarchy)
            {
                _BreadBasePool[i].transform.position = orientation.position;
                _BreadBasePool[i].transform.rotation = orientation.rotation;
                _BreadBasePool[i].SetActive(true);
                return _BreadBasePool[i];
            }
            else
                Debug.Log($"Nothing left in the pool");
        }
        return null;
    }
}
