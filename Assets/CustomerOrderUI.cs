using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerOrderUI : MonoBehaviour
{
    public bool isOrderUp = false;
    [SerializeField] Queue<Customer> _customerQueue = new Queue<Customer>();

    public IntEventChanelSO ScoreEvent;
    [SerializeField] int _score = 0;

    //public GameObject textBoxContainer;
    //public GameObject textBoxPrefab;
    public TextMeshProUGUI IngredientsBox;
    public TextMeshProUGUI scoreBox;

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
        InsertScore();
        if (number > 0)
            isOrderUp = false;
    }

    private void Awake()
    {
        IngredientsBox = GetComponent<TextMeshProUGUI>();
        if (IngredientsBox == null)
            IngredientsBox = GameObject.FindGameObjectWithTag("IngredientsText").GetComponent<TextMeshProUGUI>();
        scoreBox = GetComponent<TextMeshProUGUI>();
        if (scoreBox == null)
            scoreBox = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    void InsertScore()
    {
        if(scoreBox == null)
        {
            Debug.Log("scoreboxGO is null ");
            return;
        }
        
        scoreBox.text = $"<b><u>Score</b></u><br> ${_score}.00";
        if (_score != 0)
            DisplayOrder();
    }

    public void QueueCustomerOrder(Customer customer)
    {
        _customerQueue.Enqueue(customer);
        if (isOrderUp == false)
            DisplayOrder();
            InsertScore();
    }

    //private void DisplayOrder()
    //{
    //    Customer customer = _customerQueue.Dequeue();
    //    for (int i = 0; i < customer.SandwhichRequest.Count; i++)
    //    {
    //        GameObject textBoxGO = Instantiate(textBoxPrefab, textBoxContainer.transform);
    //        TextMeshProUGUI textBoxTMP = textBoxGO.GetComponentInChildren<TextMeshProUGUI>();
    //        textBoxTMP.text = customer.SandwhichRequest[i];
    //    }
    //}

    private void DisplayOrder()
    {
        if(_customerQueue.Count ==0)
        {
            Debug.Log("No customers in Queue");
            return;
        }
        
        Customer customer = _customerQueue.Dequeue();
        string displayText = "";

        List<string> reverseList = new List<string>(customer.SandwhichRequest);
        reverseList.Reverse();

        for(int i = 0; i < reverseList.Count; i++)
        {
            displayText += reverseList[i];
            displayText += "<br>";
        }

        //for (int i = 0; i < customer.SandwhichRequest.Count; i++)
        //{
        //    displayText += customer.SandwhichRequest[i];
        //    displayText += "<br>";
        //}
        IngredientsBox.text = displayText;
        isOrderUp = true;
    }
}
