using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.Views
{
    public class EndGameView : IUIWindow, IInitializable
    {
    private GameObject _endGameUI;
    private Button _replayButton;

    public event Action OnStartButtonPressed;

    public EndGameView(GameObject endGameUI, Button replayButton)
    {
        _endGameUI = endGameUI;
        _replayButton = replayButton;
    }

    public void Initialize()
    {
        _replayButton.onClick.AddListener(Replay);
    }

    public void ShowWindow()
    {
        _endGameUI.SetActive(true);
    }

    public void HideWindow()
    {
        _endGameUI.SetActive(false);
    }

    private void Replay()
    {
        OnStartButtonPressed?.Invoke();
    }
    
    }
}