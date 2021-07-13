using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIHandler : MonoBehaviour
{
    public GameManagerEventChannelSO startButton;
    public GameManagerEventChannelSO QuitButton;
    public void StartButtonClicked()
    {
        startButton.RaiseEvent();
    }
    public void QuitButtonClicked()
    {
        QuitButton.RaiseEvent();
    }


}
