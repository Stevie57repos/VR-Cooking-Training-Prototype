using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomerOrderUI : MonoBehaviour
{
    public bool isOrderUp = false;
    [SerializeField] private Queue<Customer> _customerQueue = new Queue<Customer>();

    public TextMeshProUGUI IngredientsBox;
    public TextMeshProUGUI ScoreBox;

    [SerializeField] private PlayerScore _playerScoreSO;
    [SerializeField] private Transform _ingredientsCanvasUI;
    [SerializeField] private GameObject _imagePrefab;
    private Dictionary<string, Sprite> _toppingsDict = new Dictionary<string, Sprite>();
    [SerializeField] private Sprite _breadImageSprite;
    [SerializeField] private Sprite _lettuceImageSprite;
    [SerializeField] private Sprite _tomatoesImageSprite;
    [SerializeField] private Sprite _hamImageSprite;
    [SerializeField] private List<GameObject> _toppingsListImages = new List<GameObject>();
    [SerializeField] private int _amountToPool;
    private void Awake()
    {
        IngredientsBox = GetComponent<TextMeshProUGUI>();
        if (IngredientsBox == null)
            IngredientsBox = GameObject.FindGameObjectWithTag("IngredientsText").GetComponent<TextMeshProUGUI>();
        ScoreBox = GetComponent<TextMeshProUGUI>();
        if (ScoreBox == null)
            ScoreBox = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        _playerScoreSO.ScoreUI = this;
        SetupDictionary();
        CreateDisplayPool();
    }
    private void SetupDictionary()
    {
        _toppingsDict.Add("Bread", _breadImageSprite);
        _toppingsDict.Add("Lettuce", _lettuceImageSprite);
        _toppingsDict.Add("Tomatoes", _tomatoesImageSprite);
        _toppingsDict.Add("Ham", _hamImageSprite);
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
        ClearDisplay();
        DisplayText();
        AddBreadImage();     
        List<string> reverseList = new List<string>(customer.SandwhichRequest);
        reverseList.Reverse();      
        for(int i = 0; i < reverseList.Count; i++)
        {
            //displayText += reverseList[i];
            //displayText += "<br>";
            GameObject toppingImageGO = RetrieveDisplay();
            //_toppingsListImages.Add(toppingImageGO);
            Image _image = toppingImageGO.GetComponentInChildren<Image>();
            _image.sprite = _toppingsDict[reverseList[i]];
            toppingImageGO.SetActive(true);
        }
        isOrderUp = true;
        AddBreadImage();
    }
    private void DisplayText()
    {
        string displayText = "<u>Toppings Request</u><br>";
        IngredientsBox.text = displayText;
    }
    private void AddBreadImage()
    {
        GameObject toppingImageGO = RetrieveDisplay();
        //if (!toppingImageGO.GetComponent<Image>());
        //{
        //    Debug.Log($"trying to get image componenet failed");
        //    toppingImageGO.GetComponent<image>
        //}         
        Image _image = toppingImageGO.GetComponentInChildren<Image>();        
        _image.sprite = _toppingsDict["Bread"];
        toppingImageGO.SetActive(true);
    }
    private void ClearDisplay()
    {
        if (_toppingsListImages.Count == 0)
            return;

        foreach (GameObject topping in _toppingsListImages)
            topping.SetActive(false); 
    }
   private void CreateDisplayPool()
    {
        for(int i = 0; i < _amountToPool; i++)
        {
            GameObject toppingImageGO = Instantiate(_imagePrefab, _ingredientsCanvasUI);
            toppingImageGO.SetActive(false);
            _toppingsListImages.Add(toppingImageGO);
        }
    }
    private GameObject RetrieveDisplay() 
    { 
        for(int i = 0; i < _toppingsListImages.Count; i++)
        {
            if (!_toppingsListImages[i].activeInHierarchy)
            {
                return _toppingsListImages[i];
            }     
        }
        return null;
    }
}
