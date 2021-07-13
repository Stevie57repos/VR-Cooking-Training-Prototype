using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Misc/Customer Values SO")]
public class CustomerDefaultValuesSO : ScriptableObject
{
    public int BaseCustomerValue; 
    public int BaseValuePerTopping; 
    [Range(1, 4)] 
    public int MinToppings;
    [Range(1, 4)] 
    public int MaxToppings;
}
