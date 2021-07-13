using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CannonLoader : MonoBehaviour
{
    public CannonHandler Canon;
    [SerializeField] AudioSource _canonLoader;
    [SerializeField] AudioClip _LoadCanonClip;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<CannonProjectile>())
        {
            Canon.AddToQueue(other.gameObject);
            _canonLoader.PlayOneShot(_LoadCanonClip);
        }
    }
}
