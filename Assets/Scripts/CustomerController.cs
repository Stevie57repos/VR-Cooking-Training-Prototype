using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField] GameObject CustomerPrefab;
    [SerializeField] List<Transform> _customerSpawnLocations = new List<Transform>();
    [SerializeField] List<int> UsedSpawnLocations = new List<int>();

    Queue<Customer> _customerQueue = new Queue<Customer>();
    private int InitialSpawnCount = 3;

    public PlayerScore PlayerScoreSO;

    private void Start()
    {
        InitialSpawn();
    }

    private void InitialSpawn()
    {
        for (int i = 0; i < InitialSpawnCount; i++)
        {
            List<int> SpawnedLocations = new List<int>();
            SpawnCustomer();
        }
        ActivateCustomer();     
    }
    public void SpawnCustomer()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        int randomLocation = UnityEngine.Random.Range(0, _customerSpawnLocations.Count);
        while (UsedSpawnLocations.Contains(randomLocation))
        {
            randomLocation = UnityEngine.Random.Range(0, _customerSpawnLocations.Count);
        }
        UsedSpawnLocations.Add(randomLocation);
        GameObject customerGO = Instantiate(CustomerPrefab, _customerSpawnLocations[randomLocation]);
        Customer newCustomer = customerGO.GetComponent<Customer>();
        newCustomer.CustController = this;
        newCustomer.SpawnLocation = randomLocation;
        customerGO.SetActive(false);
        _customerQueue.Enqueue(newCustomer);
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
        UsedSpawnLocations.Remove(location);
    }
}
