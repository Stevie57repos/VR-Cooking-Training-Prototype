using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerParticlesHandler : MonoBehaviour
{
    public CorrectOrderEffect CorrectOrderSO;
    public ParticleSystem PS;


    private void OnEnable()
    {
        CorrectOrderSO.OrderComplete += RelocateParticles;
    }

    private void OnDisable()
    {
        CorrectOrderSO.OrderComplete -= RelocateParticles;
    }

    private void Awake()
    {
        if (PS == null)
            PS = GetComponentInChildren<ParticleSystem>();
    }

    void RelocateParticles(Transform location)
    {
        transform.position = location.position;
        StartCoroutine(PlayParticles());
    }

    IEnumerator PlayParticles()
    {
        PS.Play();
        yield return new WaitForSeconds(3f);
        PS.Stop();
    }

}
