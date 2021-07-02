using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CanonLoader : MonoBehaviour
{
    public CanonHandler Canon;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<CanonProjectile>())
        {
            Canon.AddToQueue(other.gameObject);
        }
    }
}
