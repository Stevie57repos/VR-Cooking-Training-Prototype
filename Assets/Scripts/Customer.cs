using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] int _customerValue;
    int minCustomerValue = 5;
    [SerializeField] int _valuePerTopping;

    [Range(1, 4)]
    [SerializeField] int _minToppings;
    [Range(1,4)]
    public int _maxToppings;

    public List<string> SandwhichRequest = new List<string>();
    public int wrongOrderCount = 0;
    public List<string> ToppingsList = new List<string>() { "Lettuce", "Tomatoes", "Ham"};
    
    //public CustomerOrderUI OrderUI;
    public IntEventChanelSO OrderCompleteEvent;
    public CustomerController CustController;

    [SerializeField] SandwhichSO _sandwhichSO;
    public PlayerScore PlayerScoreSO;

    public List<GameObject> characterPrefabs = new List<GameObject>();

    public int SpawnLocation;

    private Rigidbody rb;

    public CorrectOrderEffect CorrectOrderEffectSO;

    [Header("Audio")]
    public AudioEventChannelSO CustomerEffectsSound;
    public AudioClip SoundSuccess;
    public AudioClip SoundFail;

    private void Awake()
    {
        if(_sandwhichSO == null)
            RequestRandomToppings();
        else
            LoadSandwhichSO();

        //rb = GetComponent<Rigidbody>();

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
        if (_sandwhichSO.isLettuceOn)
            SandwhichRequest.Add("Lettuce");
        if (_sandwhichSO.isTomatoesOn)
            SandwhichRequest.Add("Tomatoes");
        if (_sandwhichSO.isHamOn)
            SandwhichRequest.Add("Ham");
        if (_sandwhichSO.isComplete)
            SandwhichRequest.Add("Bread");
    }

    private void RequestRandomToppings()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        int toppingNumber = UnityEngine.Random.Range(_minToppings, _maxToppings);
        for(int i = 0; i < toppingNumber; i++)
        {
            int topping = UnityEngine.Random.Range(0, ToppingsList.Count);
            SandwhichRequest.Add(ToppingsList[topping]);
        }
        _customerValue = minCustomerValue + (toppingNumber * _valuePerTopping);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<SandwhichHandler>())
        {
            if (CheckSandwhich(other.gameObject.GetComponent<SandwhichHandler>()))
            {
                PlayerScoreSO.ChangeScore(_customerValue);
                CustController.ReleaseSpawnLocation(SpawnLocation);
                CustController.ActivateCustomer();
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                CorrectOrderEffectSO.RaiseEvent(this.transform);
                CustomerEffectsSound.RaiseEvent(SoundSuccess);
            }
            else
            {
                Debug.Log($"Wrong Order");
                wrongOrderCount++;
                CustomerEffectsSound.RaiseEvent(SoundFail);
                Destroy(other.gameObject);
                if(wrongOrderCount > 1)
                {
                    CustController.ReleaseSpawnLocation(SpawnLocation);
                    CustController.ActivateCustomer();
                    Destroy(this.gameObject);
                }
            }                       
        }
    }

    private bool CheckSandwhich(SandwhichHandler sandwhich)
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
