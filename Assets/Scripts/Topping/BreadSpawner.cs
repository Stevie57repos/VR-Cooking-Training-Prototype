using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class BreadSpawner : ToppingsSpawner
{
    public GameObject BreadBasePrefab;
    public SandwichHandler BreadBase ;
    [SerializeField] private List<GameObject> _breadBasePool = new List<GameObject>();
    bool canSpawn = true;

    [Header("Debug Settings")]
    [SerializeField] private bool isDebug;
    [SerializeField] private int debugPoolAmount;

    protected override void OnEnable()
    {
        base.OnEnable();
        _levelLoaded.GameManagerEvent += CreateBreadBasePool;
        _EndLevelLoaded.GameManagerEvent += ClearBreadBasePool;
        if (isDebug)
            CreateBreadBasePool(debugPoolAmount);
    }

    protected override void OnDisable()
    {
        _levelLoaded.GameManagerEvent -= CreateBreadBasePool;
        _EndLevelLoaded.GameManagerEvent -= ClearBreadBasePool;
        base.OnDisable();
    }
    private void ClearBreadBasePool()
    {
        _breadBasePool.Clear();
    }
    protected override void Awake()
    {
        base.Awake();
    }
    private void CreateBreadBasePool()
    {
        for(int i = 0; i < _objectPoolAmount; i ++)
        {
            GameObject breadBaseGO = Instantiate(BreadBasePrefab);
            breadBaseGO.SetActive(false);
            _breadBasePool.Add(breadBaseGO);
        }
    }
    private void CreateBreadBasePool(int debugAmount)
    {
        for (int i = 0; i < debugAmount; i++)
        {
            GameObject breadBaseGO = Instantiate(BreadBasePrefab);
            breadBaseGO.SetActive(false);
            _breadBasePool.Add(breadBaseGO);
        }
    }
    protected override void CreateAndSelectTopping(SelectEnterEventArgs args)
    {
        if(BreadBase == null)
        {
            CreateAndSelectBreadBase(args);
        }
        else
            base.CreateAndSelectTopping(args);
    }
    private void CreateAndSelectBreadBase(SelectEnterEventArgs args)
    {
        GameObject breadBaseGO = RetrieveBreadBase(args.interactor.transform);
        BreadBase = breadBaseGO.GetComponent<SandwichHandler>();
        BreadBase.BreadSpawner = this;    
        XRGrabInteractable breadBaseGrab = breadBaseGO.GetComponent<XRGrabInteractable>();
        interactionManager.ForceSelect(args.interactor, breadBaseGrab);
    }
    private GameObject RetrieveBreadBase(Transform orientation)
    {
        for (int i = 0; i < _breadBasePool.Count; i++)
        {
            if (!_breadBasePool[i].activeInHierarchy)
            {
                _breadBasePool[i].transform.position = orientation.position;
                _breadBasePool[i].transform.rotation = orientation.rotation;
                _breadBasePool[i].SetActive(true);
                return _breadBasePool[i];
            }
            else
                Debug.Log($"Nothing left in the pool");
        }
        return null;
    }
}
