using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Timer : MonoBehaviour
{
    [SerializeField] private UnityEvent onTimerEnd = new UnityEvent();

    public void StartTimer(float duration) => StartCoroutine(BeginTimer(duration));   

    private IEnumerator BeginTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        onTimerEnd?.Invoke();
    }

    public void stopTimer()
    {
        StopAllCoroutines();
    }
}

