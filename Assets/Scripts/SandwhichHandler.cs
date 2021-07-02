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

    //public Queue<string> CurrentToppings = new Queue<string>();
    public List<string> CurrentToppings = new List<string>();
    public List<string> TestSandwhich = new List<string>();

    private void Awake()
    {
        LoadToppingsDict();
        if(_sandwhichSO != null)
        {
            LoadSandwhichSO();
        }
    }

    //private void LoadToppingsDict()
    //{
    //    ToppingsDict.Add("Lettuce", _lettuce);
    //    ToppingsDict.Add("Tomatoes", _tomatoes);
    //    ToppingsDict.Add("Ham", _ham);
    //    ToppingsDict.Add("Bread", _BreadTopping);
    //}

    private void LoadToppingsDict()
    {
        ToppingsDict.Add("Lettuce", _lettucePrefab);
        ToppingsDict.Add("Tomatoes", _tomatoesPrefab);
        ToppingsDict.Add("Ham", _hamPrefab);
        ToppingsDict.Add("Bread", _BreadToppingPrefab);
    }

    private void LoadSandwhichSO()
    {
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

    //private void LoadSandwhichSO()
    //{
    //    if (_sandwhichSO.isLettuceOn)
    //    {
    //        GameObject lettucceGO = Instantiate(ToppingsDict["Lettuce"]);
    //        AddTopping("Lettuce", lettucceGO);
    //    }
    //    if (_sandwhichSO.isTomatoesOn)
    //    {
    //        GameObject tomatoesGO= Instantiate(ToppingsDict["Tomatoes"]);
    //        AddTopping("Tomatoes", tomatoesGO);
    //    }
    //    if (_sandwhichSO.isHamOn)
    //    {
    //        GameObject hamGO = Instantiate(ToppingsDict["Ham"]);
    //        AddTopping("Ham", hamGO);
    //    }

    //    if (_sandwhichSO.isComplete)
    //    {
    //        GameObject breadGO = Instantiate(ToppingsDict["Bread"]);
    //        AddTopping("Bread", breadGO);
    //    }        

    //}

    //public void AddTopping(string toppingName)
    //{
    //    if (!isComplete)
    //    {
    //        CurrentToppings.Add(toppingName);
    //        if (toppingName == "Bread")
    //        {
    //            isComplete = true;
    //        }
    //        toppingCount++;
    //        GameObject topping = ToppingsDict[toppingName];
    //        Vector3 toppingPosition = new Vector3(transform.position.x, transform.position.y + (toppingHeight * toppingCount), transform.position.z);
    //        topping.transform.position = toppingPosition;
    //        topping.SetActive(true);
    //    }
    //}

    public void AddTopping(string toppingName)
    {
        if (!isComplete)
        {
            CurrentToppings.Add(toppingName);
            if (toppingName == "Bread")
            {
                isComplete = true;
            }
            toppingCount++;
            GameObject topping = Instantiate(ToppingsDict[toppingName]);
            //Debug.Log($"local scale is {topping.transform.localScale.x} {topping.transform.localScale.y} {topping.transform.localScale.z} ");
            topping.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            //Debug.Log($"local scale is now {topping.transform.localScale.x} {topping.transform.localScale.y} {topping.transform.localScale.z} ");
            topping.SetActive(true);
            //topping.GetComponent<SandwhichToppingHandler>().enabled = false;
            //if (topping.GetComponent<BoxCollider>())
            //{
            //    topping.GetComponent<BoxCollider>().enabled = false;
            //}
            //else if (topping.GetComponent<CapsuleCollider>())
            //{
            //    topping.GetComponent<CapsuleCollider>().enabled = false;
            //}
            topping.transform.parent = this.gameObject.transform;
            //Vector3 toppingPosition = new Vector3(transform.position.x, transform.position.y + (toppingHeight * toppingCount), transform.position.z);
            //Vector3 toppingPosition = new Vector3(transform.localPosition.x , (toppingHeight * toppingCount), 0);       
            topping.transform.localPosition = new Vector3(0, (toppingHeight * toppingCount), 0);
            
            //Debug.Log($"topping position is {toppingPosition}");
            //topping.transform.position = toppingPosition;
            //topping.SetActive(true);
        }
    }
}
