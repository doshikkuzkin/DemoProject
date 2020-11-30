using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.GameControllers
{
    public class EndGameUIController : IInitializable
    {
    private GameObject _endGameUI;
    private Button _replayButton;

    public event Action OnPlayAgainButtonPressed;

    public EndGameUIController(GameObject endGameUI, Button replayButton)
    {
        _endGameUI = endGameUI;
        _replayButton = replayButton;
    }

    public void Initialize()
    {
        _replayButton.onClick.AddListener(Replay);
    }

    public void ShowUI()
    {
        _endGameUI.SetActive(true);
    }

    private void Replay()
    {
        _endGameUI.SetActive(false);
        OnPlayAgainButtonPressed?.Invoke();
    }
    
    }
}