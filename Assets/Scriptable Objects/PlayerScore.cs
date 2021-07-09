using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "PlayerScoreSO")]
public class PlayerScore : ScriptableObject
{
    public CustomerOrderUI ScoreUI;
    public int Score;

    private void OnEnable()
    {
        Score = 0;
    }

    public void ChangeScore(int value)
    {
        ScoreUI.ChangeScore(value);
    }

    public void DisplayCustomerOrder(Customer customer)
    {
        ScoreUI.DisplayOrder(customer);
    }
}
