using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{   
    public ScreenFade screenFade = null;
    public int _score = 0;

    public IntEventChanelSO ScoreEvent;

    private void OnEnable()
    {
        ScoreEvent.orderCompleteEvent += ChangeScore;
    }

    private void OnDisable()
    {
        ScoreEvent.orderCompleteEvent -= ChangeScore;
    }

    private void ChangeScore(int number)
    {
        _score += number;
    }

    private IEnumerator FadeSequence()      
    {
        // fade to black
        float duration = screenFade.FadeIn();

        // wait
        yield return new WaitForSeconds(duration);
        yield return new WaitForSeconds(1f);
        // fade to clear
        screenFade.FadeOut();
    }

    private void OnValidate()
    {
        if (!screenFade)
            screenFade = FindObjectOfType<ScreenFade>();
    }

}
