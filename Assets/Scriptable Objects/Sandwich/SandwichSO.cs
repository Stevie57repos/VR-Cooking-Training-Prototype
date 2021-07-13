using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SandwhichSO")]
public class SandwichSO : ScriptableObject
{
    public bool isComplete = false;
    public bool isLettuceOn = false;
    public bool isTomatoesOn = false;
    public bool isHamOn = false;

    public List<string> ToppingsList= new List<string>();

    private void OnEnable()
    {
        ToppingsList.Clear();
        SetToppingList();
    }

    public void SetToppingList()
    {
        if(isLettuceOn)
            ToppingsList.Add("Lettuce");
        if(isTomatoesOn)
            ToppingsList.Add("Tomatoes");
        if(isHamOn)
            ToppingsList.Add("Ham");
        if (isComplete)
            ToppingsList.Add("Complete");
    }
}
