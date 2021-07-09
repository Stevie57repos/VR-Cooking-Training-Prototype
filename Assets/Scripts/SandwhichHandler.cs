using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwhichHandler : MonoBehaviour
{
    [SerializeField] SandwhichSO _sandwhichSO;

    public int toppingCount = 0;
    [Header("Toppings Check")]
    public bool isComplete = false;
    [SerializeField] float toppingHeight;

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
        if(_sandwhichSO != null)
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

    private void LoadSandwhichSO()
    {
        //For testing - Add toppings based on scriptable object
        if (_sandwhichSO.isLettuceOn)
        {
            GameObject lettucceGO = Instantiate(ToppingsDict["Lettuce"]);
            AddTopping("Lettuce");
        }
        if (_sandwhichSO.isTomatoesOn)
        {
            GameObject tomatoesGO = Instantiate(ToppingsDict["Tomatoes"]);
            AddTopping("Tomatoes");
        }
        if (_sandwhichSO.isHamOn)
        {
            GameObject hamGO = Instantiate(ToppingsDict["Ham"]);
            AddTopping("Ham");
        }

        if (_sandwhichSO.isComplete)
        {
            GameObject breadGO = Instantiate(ToppingsDict["Bread"]);
            AddTopping("Bread");
        }

    }
    public void AddTopping(string toppingName)
    {
        if (!isComplete)
        {
            CurrentToppings.Add(toppingName);
            if (toppingName == "Bread")
            {
                isComplete = true;
                BreadSpawner.breadBase = null;
            }
            toppingCount++;
            GameObject topping = Instantiate(ToppingsDict[toppingName]);
            topping.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            topping.SetActive(true);
            topping.transform.parent = this.gameObject.transform;   
            topping.transform.localPosition = new Vector3(0, (toppingHeight * toppingCount), 0);            
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if(!isOnGround)
                _timer.StartTimer(6f);
        }
        else if (other.gameObject.CompareTag("Building"))
        {
            DestroySandwhich();
        }
    }

    public void PickedUp()
    {
        _timer.stopTimer();
        isOnGround = false;
    }

    public void DestroySandwhich()
    {
        BreadSpawner.breadBase = null;
        Destroy(this.gameObject);
    }
}
