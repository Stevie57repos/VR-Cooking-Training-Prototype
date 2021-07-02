using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SandwhichOrdersSO")]
[System.Serializable]
public class SandwhichOrdersSO : ScriptableObject
{
    public List<List<string>> SandwhichOrders = new List<List<string>>();
}
