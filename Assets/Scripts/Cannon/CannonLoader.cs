using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CannonLoader : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CannonHandler Canon;
    [SerializeField] AudioSource _canonLoader;
    [SerializeField] AudioClip _LoadCanonClip;
    [SerializeField] private GameManagerEventChannelSO fireCanon;
    [SerializeField] private Timer _timer;
    [SerializeField] private bool _isOpen;
    [SerializeField] private BoxCollider _boxCollider;
    void Start()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
        if (_timer == null)
            _timer = GetComponent<Timer>();
        _isOpen = false;
    }
    private void OnEnable()
    {
        fireCanon.GameManagerEvent += CloseCannonReloader;
    }
    private void OnDisable()
    {
        fireCanon.GameManagerEvent -= CloseCannonReloader;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<CannonProjectile>())
        {
            Canon.AddToQueue(other.gameObject);
            _canonLoader.PlayOneShot(_LoadCanonClip);
           // _boxCollider.enabled = false;
        }
    }
    public void OpenCannonReloader()
    {
        Debug.Log("open cannon called ");
        _isOpen = true;
        _animator.SetBool("isOpen", _isOpen);
    }
    public void CloseCannonReloader()
    {
        if(_isOpen == true)
        {
            _isOpen = false;    
            _animator.SetBool("isOpen", _isOpen);
            //_boxCollider.enabled = true;
        }
    }
}
