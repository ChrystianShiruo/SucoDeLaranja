using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {


    [Header("Counters")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _matchesText;
    [SerializeField] private TextMeshProUGUI _turnsText;

    private GameData _gameData;
    private Action<int> _scoreChangeActions;
    private Action<int> _turnsChangeActions;
    private Action<int> _matchesChangeActions;

    private void OnDestroy() {
        if(_scoreChangeActions != null) {
            _gameData.OnScoreChange -= _scoreChangeActions;
            _scoreChangeActions = null;
        }
        if(_turnsChangeActions != null) {
            _gameData.OnTurnsChange -= _turnsChangeActions;
            _turnsChangeActions = null;
        }
        if(_matchesChangeActions != null) {
            _gameData.OnMatchesChange -= _matchesChangeActions;
            _matchesChangeActions = null;
        }
    }

    public void Init(GameData gameData) {
        OnDestroy();

        _gameData = gameData;
        _scoreChangeActions += SetScoreText;
        _gameData.OnScoreChange += _scoreChangeActions;

        _turnsChangeActions += SetTurnsText;
        _gameData.OnTurnsChange += _turnsChangeActions;

        _matchesChangeActions += SetMatchesText;
        _gameData.OnMatchesChange += _matchesChangeActions;
        UpdateCounters(_gameData);
    }

    public void LoadGame() {
        GameController.Instance.LoadGame();
    }
    public void SaveGame() {
        GameController.Instance.SaveGame();
    }
    public void NewGame() {
        GameController.Instance.NewGame();
    }



    private void SetScoreText(int value) {
        UpdateText(_scoreText, $"{value}");
    }

    private void SetMatchesText(int value) {
        UpdateText(_matchesText, $"{value}/{_gameData.Pairs}");
    }

    private void SetTurnsText(int value) {
        UpdateText(_turnsText, $"{value}");
    }

    private void UpdateText(TextMeshProUGUI textComponent, string value) {
        textComponent.text = value;
    }

    private void UpdateCounters(GameData gameData) {
        SetScoreText(gameData.Score);
        SetMatchesText(gameData.Matches);
        SetTurnsText(gameData.Turns);
    }
}