using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    private Transform _target;
    private int _state;
    private Vector3 _regularSize;
    [SerializeField] private bool _isIndicating;  
    [SerializeField] private Vector3 _largeSize;
    [SerializeField] private float _sizeMultipler = 1.2f;
    [SerializeField] private WaitForSeconds ChangeRate = new WaitForSeconds(1f);

    private void Awake()
    {
        _target = this.transform;
        _regularSize = this.transform.localScale;
        _largeSize = new Vector3(_regularSize.x * _sizeMultipler, _regularSize.y * _sizeMultipler, _regularSize.z * _sizeMultipler);
    }

    private void Start()
    {
        StartCoroutine(TargetIndicator());
    }

    IEnumerator TargetIndicator()
    {
        while (_isIndicating)
        {
            if(_state == 0)
            {
                _target.localScale = _largeSize;
                _state++;
            }
            else if(_state == 1) 
            {
                _target.localScale = _regularSize;
                _state = 0;
            }
            yield return ChangeRate;
        }
    }
}
