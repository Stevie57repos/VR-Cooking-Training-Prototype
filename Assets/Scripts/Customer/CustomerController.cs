using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private GameObject _customerPrefab;
    [SerializeField] private List<Transform> _customerSpawnLocations = new List<Transform>();
    [SerializeField] private List<int> _availableSpawnLocations = new List<int>();

    private Queue<Customer> _customerQueue = new Queue<Customer>();
    private int _initialSpawnCount = 6;

    public PlayerScore PlayerScoreSO;

    private void Awake()
    {
        SetUpSpawnLocationList();
    }
    private void SetUpSpawnLocationList()
    {
        int count = 0;
        for(int i = 0; i < _customerSpawnLocations.Count; i++)
        {
            _availableSpawnLocations.Add(count);
            count++;
        }
    }
    private void Start()
    {
        InitialSpawn();
    }
    private void InitialSpawn()
    {
        for (int i = 0; i < _initialSpawnCount; i++)
        {
            SpawnCustomer();
        }
        ActivateCustomer();     
    }
    public void SpawnCustomer()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        int randomLocation = _availableSpawnLocations[UnityEngine.Random.Range(0, _availableSpawnLocations.Count - 1)];
        Debug.Log($" randomlocation is {randomLocation}");
        GameObject customerGO = Instantiate(_customerPrefab, _customerSpawnLocations[randomLocation]);
        Customer newCustomer = customerGO.GetComponent<Customer>();
        newCustomer.CustController = this;
        newCustomer.SpawnLocation = randomLocation;
        customerGO.SetActive(false);
        _customerQueue.Enqueue(newCustomer);      
        _availableSpawnLocations.RemoveAt(randomLocation);
    }
    public void ActivateCustomer()
    {
        if(_customerQueue.Count == 0)
        {
            Debug.Log($"there are no more customers in Queue");
            SpawnCustomer();
        }
        var customer = _customerQueue.Dequeue();
        customer.gameObject.SetActive(true);
        PlayerScoreSO.DisplayCustomerOrder(customer);
    }
    public void ReleaseSpawnLocation(int location)
    {
        _availableSpawnLocations.Add(location);
    }
}
