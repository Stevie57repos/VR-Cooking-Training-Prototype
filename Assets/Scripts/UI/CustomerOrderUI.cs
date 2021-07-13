using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CustomerOrderUI : MonoBehaviour
{
    public bool isOrderUp = false;
    [SerializeField] Queue<Customer> _customerQueue = new Queue<Customer>();

    public TextMeshProUGUI IngredientsBox;
    public TextMeshProUGUI scoreBox;

    public PlayerScore PlayerScoreSO;

    private void Awake()
    {
        IngredientsBox = GetComponent<TextMeshProUGUI>();
        if (IngredientsBox == null)
            IngredientsBox = GameObject.FindGameObjectWithTag("IngredientsText").GetComponent<TextMeshProUGUI>();
        scoreBox = GetComponent<TextMeshProUGUI>();
        if (scoreBox == null)
            scoreBox = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        PlayerScoreSO.ScoreUI = this;
    }
    public void ChangeScore(int number)
    {
        PlayerScoreSO.Score += number;
        InsertScore();
    }
    void InsertScore()
    {
        if(scoreBox == null)
        {
            Debug.Log("scoreboxGO is null ");
            return;
        }      
        scoreBox.text = $"<b><u>Score</b></u><br> ${PlayerScoreSO.Score}.00";
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
