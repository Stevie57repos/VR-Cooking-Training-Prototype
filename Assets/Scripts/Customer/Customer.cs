using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [Header("Customer Values and Toppings settings")]
    private int _customerValue;
    public List<string> SandwhichRequest = new List<string>();
    public List<string> ToppingsList = new List<string>() { "Lettuce", "Tomatoes", "Ham"};
    public int SpawnLocation;
    public CustomerController CustController;
    private int _wrongOrderCount = 0;

    [Header("Events")]
    public IntEventChanelSO OrderCompleteEvent;
    public AudioEventChannelSO CustomerEffectsSound;

    [Header("Customer Models")]
    public List<GameObject> CharacterPrefabs = new List<GameObject>();

    [Header("Scriptable Objects")]
    [SerializeField] PlayerScore _playerScoreSO;
    [SerializeField] CustomerDefaultValuesSO _baseValuesSO;
    [SerializeField] private SandwichSO _debugSandwichSO;
    [SerializeField] CorrectOrderEffect _correctOrderEffectSO;

    [Header("Audio")]
    public AudioClip SoundSuccess;
    public AudioClip SoundFail;
    private void Awake()
    {
        if(_debugSandwichSO == null)
            RequestRandomToppings();
        else
            LoadSandwhichSO();

        LoadCharacterModel();
    }
    private void LoadCharacterModel()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        int randomModel = UnityEngine.Random.Range(0, CharacterPrefabs.Count);
        Instantiate(CharacterPrefabs[randomModel], this.transform);
    }
    private void LoadSandwhichSO()
    {
        if (_debugSandwichSO.ToppingsList.Count == 0)
            _debugSandwichSO.SetToppingList();
        foreach(string topping in _debugSandwichSO.ToppingsList)
        {
            if (topping != "Complete")
                SandwhichRequest.Add(topping);
            else
                SandwhichRequest.Add("Bread");
        }
    }
    private void RequestRandomToppings()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        int toppingNumber = UnityEngine.Random.Range(_baseValuesSO.MinToppings, _baseValuesSO.MaxToppings);
        for(int i = 0; i < toppingNumber; i++)
        {
            int topping = UnityEngine.Random.Range(0, ToppingsList.Count);
            SandwhichRequest.Add(ToppingsList[topping]);
        }
        _customerValue = _baseValuesSO.BaseCustomerValue + (toppingNumber * _baseValuesSO.BaseValuePerTopping);
    }
    private void OnCollisionEnter(Collision other)
    {
        SandwichHandler sandwhich = other.gameObject.GetComponent<SandwichHandler>();

        if(sandwhich != null)
        {
            if (CheckSandwhich(sandwhich))
            {
                _playerScoreSO.ChangeScore(_customerValue);
                CustController.ReleaseSpawnLocation(SpawnLocation);
                CustController.ActivateCustomer();
                sandwhich.ResetSandwich();
                _correctOrderEffectSO.RaiseEvent(this.transform);
                CustomerEffectsSound.RaiseEvent(SoundSuccess);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log($"Wrong Order");
                _wrongOrderCount++;
                CustomerEffectsSound.RaiseEvent(SoundFail);
                sandwhich.ResetSandwich();
                if (_wrongOrderCount > 1)
                {
                    CustController.ReleaseSpawnLocation(SpawnLocation);
                    CustController.ActivateCustomer();
                    Destroy(this.gameObject);
                }
            }                       
        }
    }
    private bool CheckSandwhich(SandwichHandler sandwhich)
    {
        if (sandwhich.CurrentToppings.Count == 0)
            return false;

        for (int i = 0; i < SandwhichRequest.Count; i++)
        {
            if (sandwhich.CurrentToppings[i] != SandwhichRequest[i])
            {
                return false;
            }
        }
        return true;
    }
}
