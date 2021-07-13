using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToppingsSpawner : XRBaseInteractable
{
    public GameObject ToppingPrefab = null;
    [SerializeField] private AudioSource _toppingAudioSource;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private List<GameObject> _toppingObjectPool = new List<GameObject>();
    [SerializeField] protected int _objectPoolAmount;
    [SerializeField] protected GameManagerEventChannelSO _levelLoaded;
    [SerializeField] protected GameManagerEventChannelSO _EndLevelLoaded;
    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(CreateAndSelectTopping);
        selectEntered.AddListener(PlaySound);
        _levelLoaded.GameManagerEvent += CreateToppingObjectPool;
        _EndLevelLoaded.GameManagerEvent += ClearObjectPool;
    }
    protected override void OnDisable()
    {      
        selectEntered.RemoveListener(CreateAndSelectTopping);
        selectEntered.RemoveListener(PlaySound);
        _levelLoaded.GameManagerEvent -= CreateToppingObjectPool;
        _EndLevelLoaded.GameManagerEvent -= ClearObjectPool;
        base.OnDisable();
    }
    protected override void Awake()
    {
        base.Awake();
        CheckAudio();
    }
    private void CheckAudio()
    {
        if (_toppingAudioSource == null)
            _toppingAudioSource = GetComponent<AudioSource>();
    }
    private void CreateToppingObjectPool()
    {
        for(int i = 0; i < _objectPoolAmount; i++)
        {
            GameObject toppingGO = Instantiate(ToppingPrefab);
            toppingGO.SetActive(false);
            _toppingObjectPool.Add(toppingGO);
        }
    }
    private void ClearObjectPool()
    {
        _toppingObjectPool.Clear();
    }
    protected virtual void CreateAndSelectTopping(SelectEnterEventArgs args)
    {
        GameObject toppingGO = RetreiveTopping(args.interactor.transform);
        SandwichToppingHandler topping = toppingGO.GetComponent<SandwichToppingHandler>();
        interactionManager.ForceSelect(args.interactor, topping);
        _toppingAudioSource.PlayOneShot(_clip);
    }
    private GameObject RetreiveTopping(Transform orientation)
    {
        for(int i = 0; i < _toppingObjectPool.Count; i++)
        {
            if (!_toppingObjectPool[i].activeInHierarchy)
            {
                _toppingObjectPool[i].transform.position = orientation.position;
                _toppingObjectPool[i].transform.rotation = orientation.rotation;
                _toppingObjectPool[i].SetActive(true);
                return _toppingObjectPool[i];
            }
            else
                Debug.Log($"Nothing left in the pool");
        }
        return null;
    }
    private void PlaySound(SelectEnterEventArgs args)
    {
        _toppingAudioSource.PlayOneShot(_clip);
    }
}
