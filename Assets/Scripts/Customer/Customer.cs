using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [Header("Customer Values and Toppings settings")]
    private int _customerValue;
    public List<string> SandwhichRequest = new List<string>();
    private int wrongOrderCount = 0;
    public List<string> ToppingsList = new List<string>() { "Lettuce", "Tomatoes", "Ham"};
    public int SpawnLocation;
    public CustomerController CustController;

    [Header("Events")]
    public IntEventChanelSO OrderCompleteEvent;
    public AudioEventChannelSO CustomerEffectsSound;

    [Header("Customer Models")]
    public List<GameObject> characterPrefabs = new List<GameObject>();

    [Header("Scriptable Objects")]
    public PlayerScore PlayerScoreSO;
    public CustomerDefaultValuesSO BaseValuesSO;
    public SandwichSO _DebugSandwichSO;
    public CorrectOrderEffect CorrectOrderEffectSO;

    [Header("Audio")]
    public AudioClip SoundSuccess;
    public AudioClip SoundFail;
    private void Awake()
    {
        if(_DebugSandwichSO == null)
            RequestRandomToppings();
        else
            LoadSandwhichSO();

        LoadCharacterModel();
    }
    private void LoadCharacterModel()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        int randomModel = UnityEngine.Random.Range(0, characterPrefabs.Count);
        Instantiate(characterPrefabs[randomModel], this.transform);
    }
    private void LoadSandwhichSO()
    {
        if (_DebugSandwichSO.ToppingsList.Count == 0)
            _DebugSandwichSO.SetToppingList();
        foreach(string topping in _DebugSandwichSO.ToppingsList)
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
        int toppingNumber = UnityEngine.Random.Range(BaseValuesSO.MinToppings, BaseValuesSO.MaxToppings);
        for(int i = 0; i < toppingNumber; i++)
        {
            int topping = UnityEngine.Random.Range(0, ToppingsList.Count);
            SandwhichRequest.Add(ToppingsList[topping]);
        }
        _customerValue = BaseValuesSO.BaseCustomerValue + (toppingNumber * BaseValuesSO.BaseValuePerTopping);
    }
    private void OnCollisionEnter(Collision other)
    {
        SandwichHandler sandwhich = other.gameObject.GetComponent<SandwichHandler>();

        if(sandwhich != null)
        {
            if (CheckSandwhich(sandwhich))
            {
                PlayerScoreSO.ChangeScore(_customerValue);
                CustController.ReleaseSpawnLocation(SpawnLocation);
                CustController.ActivateCustomer();
                //Destroy(other.gameObject);
                sandwhich.ResetSandwich();
                CorrectOrderEffectSO.RaiseEvent(this.transform);
                CustomerEffectsSound.RaiseEvent(SoundSuccess);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log($"Wrong Order");
                wrongOrderCount++;
                CustomerEffectsSound.RaiseEvent(SoundFail);
                sandwhich.ResetSandwich();
                if (wrongOrderCount > 1)
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
