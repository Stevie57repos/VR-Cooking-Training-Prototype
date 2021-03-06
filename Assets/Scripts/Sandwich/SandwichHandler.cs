using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class SandwichHandler : MonoBehaviour
{
    [Header("Toppings Check")]
    public bool isComplete = false;
    [SerializeField] float _toppingHeight;
    [SerializeField] SandwichSO _DebugSandwichSO;
    private int toppingCount = 0;

    [Header("Topping Prefabs")]
    [SerializeField] GameObject _lettucePrefab;
    [SerializeField] GameObject _tomatoesPrefab;
    [SerializeField] GameObject _hamPrefab;
    [SerializeField] GameObject _BreadToppingPrefab;

    private Dictionary<string, GameObject> ToppingsDict  = new Dictionary<string, GameObject>();
    public List<string> CurrentToppings = new List<string>();
    public List<string> TestSandwhich = new List<string>();
    
    public BreadSpawner BreadSpawner;

    public bool isOnGround = false;
    private Timer _timer;

    private void Awake()
    {
        LoadToppingsDict();
        //For testing - Add toppings based on scriptable object
        if(_DebugSandwichSO != null)
        {
            LoadSandwhichSO();
        }
        if (_timer == null)
            _timer = GetComponent<Timer>();
    }
    private void LoadToppingsDict()
    {
        ToppingsDict.Add("Lettuce", _lettucePrefab);
        ToppingsDict.Add("Tomatoes", _tomatoesPrefab);
        ToppingsDict.Add("Ham", _hamPrefab);
        ToppingsDict.Add("Bread", _BreadToppingPrefab);
    }
    //For testing - Add toppings based on scriptable object
    private void LoadSandwhichSO()
    {
        if (_DebugSandwichSO.ToppingsList == null)
            _DebugSandwichSO.SetToppingList();

        foreach (string topping in _DebugSandwichSO.ToppingsList)
        {
            if (topping == "Complete")
                AddTopping("Bread");
            else
                AddTopping(topping);            
        }
    }
    public void AddTopping(string toppingName)
    {
        if(toppingName == "BreadBase")
        {
            GameObject topping = Instantiate(ToppingsDict["Bread"]);
            topping.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            topping.transform.parent = this.gameObject.transform;
            topping.transform.localPosition = new Vector3(0, 0, 0);
            topping.transform.rotation = new Quaternion(0,0,0,0);
            return;
        }

        if (!isComplete)
        {
            CurrentToppings.Add(toppingName);
            if (toppingName == "Bread")
            {
                isComplete = true;
                if(_DebugSandwichSO == null)
                    BreadSpawner.BreadBase = null;
            }
            toppingCount++;
            GameObject topping = Instantiate(ToppingsDict[toppingName]);
            topping.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            topping.SetActive(true);
            topping.transform.parent = this.gameObject.transform;
            topping.transform.rotation = Quaternion.identity;
            topping.transform.localPosition = new Vector3(0, (_toppingHeight * toppingCount), 0);            
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log($"ground occured");
            if (!isOnGround)
            {
                isOnGround = true;
                _timer.StartTimer(6f);
            }                
        }
        else if (other.gameObject.CompareTag("Building"))
        {
            Debug.Log($"Building");
            ResetSandwich();
        }
        else
        {
            Debug.Log($"I just hit something and it is {other.gameObject.name}");
        }
    }
    public void PickedUp()
    {
        _timer.stopTimer();
        isOnGround = false;
    }
    public void ResetSandwich()
    {
        Debug.Log("Resetsandwhich called");
        BreadSpawner.BreadBase = null;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;
        CurrentToppings.Clear();
        toppingCount = 0;
        isComplete = false;
        foreach (Transform child in this.transform)
            GameObject.Destroy(child.gameObject);
        AddTopping("BreadBase");
        this.gameObject.SetActive(false);
    }
}
