using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBackgroundHandler : MonoBehaviour
{
    [SerializeField] private Renderer _backRenderer;
    [SerializeField] private Renderer _frontRenderer;
    [SerializeField] private float _timeBetweenChange;
    private List<Vector4> _colorList = new List<Vector4>() { new Vector4(0, 0, 1, 1), new Vector4(1, 0, 0, 1)};
    void Start()
    {
        SelectColor();
    }
    private void SelectColor()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        int randomNumber = UnityEngine.Random.Range(0, _colorList.Count);
        Color color = _colorList[randomNumber];
        _backRenderer.material.color = color;
        _frontRenderer.material.color = color;
    }
}
