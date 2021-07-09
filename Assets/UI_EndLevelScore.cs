using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UI_EndLevelScore : MainMenuUIHandler
{
    [SerializeField] PlayerScore _playerScore;
    [SerializeField] TextMeshProUGUI scoreBox;

    private void Awake()
    {
        if (scoreBox == null)
            scoreBox = GetComponentInChildren<TextMeshProUGUI>();
    }
    void Start()
    {
        DisplayScore();
    }

    private void DisplayScore()
    {
        scoreBox.text = $"<b><u>Score</b></u><br> ${_playerScore.Score}.00";
    }



}
