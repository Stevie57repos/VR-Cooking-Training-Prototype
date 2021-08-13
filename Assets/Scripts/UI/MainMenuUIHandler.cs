using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIHandler : MonoBehaviour
{
    public GameManagerEventChannelSO StartButton;
    public GameManagerEventChannelSO QuitButton;
    public void StartButtonClicked()
    {
        StartButton.RaiseEvent();
    }
    public void QuitButtonClicked()
    {
        QuitButton.RaiseEvent();
    }
}
