using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(GameLauncher))]
public class UIManager : MonoBehaviour, IGameStartListener, IGamePausedListener, IGameResumeListener, IGameFinishListener
{
    [SerializeField] private TMP_Text _countdownLabel;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _pauseButton;

    [SerializeField] private TMP_Text _pauseButtonText;

    private GameLauncher _gameLauncher;
    private GameManager _gameManager;

    private void Awake()
    {
        _startButton.onClick.AddListener(StartButtonPressed);
        _pauseButton.onClick.AddListener(PauseButtonPressed);

        _gameManager = GameManager.Shared;
        _gameManager.AddListener(this);

        _gameLauncher = GetComponent<GameLauncher>();
        _gameLauncher.OnCountdownValueChanged += UpdateCountdownText;
        _gameLauncher.OnCountdownStarted += OnCountdownStarted;
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveListener(StartButtonPressed);
        _pauseButton.onClick.RemoveListener(PauseButtonPressed);
        _gameLauncher.OnCountdownValueChanged -= UpdateCountdownText;
    }

    private void StartButtonPressed()
    {
        _gameLauncher.LaunchGame();
    }

    private void PauseButtonPressed()
    {
        _gameManager.TogglePause();
    }

    private void UpdateCountdownText(int value)
    {
        OnCountdownStarted();
        _countdownLabel.text = (value + 1).ToString();
    }

    public void OnGamePaused()
    {
        _pauseButtonText.text = "Продолжить";
    }

    public void OnGameResumed()
    {
        _pauseButtonText.text = "Пауза";
    }

    public void OnGameStarted()
    {
        _startButton.gameObject.SetActive(false);
        _countdownLabel.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
    }

    public void OnGameFinished()
    {
        _countdownLabel.gameObject.SetActive(true);
        _countdownLabel.text = "Вы проиграли!";
        _pauseButton.gameObject.SetActive(false);
    }

    private void OnCountdownStarted()
    {
        _startButton.gameObject.SetActive(false);
        _countdownLabel.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
    }
}
