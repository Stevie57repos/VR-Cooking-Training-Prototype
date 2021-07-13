using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CustomerOrderUI : MonoBehaviour
{
    public bool isOrderUp = false;
    [SerializeField] private Queue<Customer> _customerQueue = new Queue<Customer>();

    public TextMeshProUGUI IngredientsBox;
    public TextMeshProUGUI ScoreBox;

    [SerializeField] private PlayerScore _playerScoreSO;

    private void Awake()
    {
        IngredientsBox = GetComponent<TextMeshProUGUI>();
        if (IngredientsBox == null)
            IngredientsBox = GameObject.FindGameObjectWithTag("IngredientsText").GetComponent<TextMeshProUGUI>();
        ScoreBox = GetComponent<TextMeshProUGUI>();
        if (ScoreBox == null)
            ScoreBox = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        _playerScoreSO.ScoreUI = this;
    }
    public void ChangeScore(int number)
    {
        _playerScoreSO.Score += number;
        InsertScore();
    }
    void InsertScore()
    {
        if(ScoreBox == null)
        {
            Debug.Log("scoreboxGO is null ");
            return;
        }      
        ScoreBox.text = $"<b><u>Score</b></u><br> ${_playerScoreSO.Score}.00";
    }
    public void DisplayOrder(Customer customer)
    {      
        string displayText = "<u>Toppings Request</u><br>";

        List<string> reverseList = new List<string>(customer.SandwhichRequest);
        reverseList.Reverse();

        for(int i = 0; i < reverseList.Count; i++)
        {
            displayText += reverseList[i];
            displayText += "<br>";
        }
        IngredientsBox.text = displayText;
        isOrderUp = true;
    }
}
