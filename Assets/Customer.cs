using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] int _customerValue;
    [Range(1, 4)]
    [SerializeField] int _minToppings;
    [Range(1,4)]
    [SerializeField] int _maxToppings;

    int minCustomerValue = 5;
    [SerializeField] int _valuePerTopping;

    public List<string> SandwhichRequest = new List<string>();
    public List<string> ToppingsList = new List<string>() { "Lettuce", "Tomatoes", "Ham"};
    
    public CustomerOrderUI OrderUI;
    public IntEventChanelSO OrderCompleteEvent;

    [SerializeField] SandwhichSO _sandwhichSO;
    private void Awake()
    {
        if(_sandwhichSO == null)
            RequestRandomToppings();
        else
            LoadSandwhichSO();

        OrderUI.QueueCustomerOrder(this);
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
                Debug.Log($"correct order !");
                OrderCompleteEvent.RaiseEvent(_customerValue);
                // trigger animation
            }
            else
            {
                Debug.Log($"Wrong Order");
                // trigger animation
            }                       
            Destroy(other.gameObject);
        }
    }

    private bool CheckSandwhich(SandwhichHandler sandwhich)
    {
        for (int i = 0; i < SandwhichRequest.Count; i++)
        {
            if (sandwhich.CurrentToppings[i] != SandwhichRequest[i])
            {
                Debug.Log("Sandwhich Doesn't Match Customer Sandwhich Request");
                return false;
            }
        }
        Debug.Log($"Sandwhich matches customer request");
        return true;
    }
}
