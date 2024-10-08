using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {



    [Header("Counters")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _matchesText;
    [SerializeField] private TextMeshProUGUI _turnsText;
    [Space(40)]
    [SerializeField] private Button _saveButton;

    private int pairs;
    private GameData _gameData;
    private Action<int> _scoreChangeActions;
    private Action<int> _turnsChangeActions;
    private Action<int> _matchesChangeActions;


    private void Start() {
        _saveButton.interactable = false;
        GameController.instance.OnChangeGameState += ToggleSaveButtonInteraction;
    }

    private void ToggleSaveButtonInteraction(GameController.GameState gameState) {
        _saveButton.interactable = gameState == GameController.GameState.Playing;
    }

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
        pairs = (_gameData.Board.Length * _gameData.Board[0].cardArray.Length) / 2;
        _scoreChangeActions += SetScoreText;
        _gameData.OnScoreChange += _scoreChangeActions;

        _turnsChangeActions += SetTurnsText;
        _gameData.OnTurnsChange += _turnsChangeActions;

        _matchesChangeActions += SetMatchesText;
        _gameData.OnMatchesChange += _matchesChangeActions;
        UpdateCounters(_gameData);
    }

    public void LoadGame() {
        GameController.instance.LoadGame();
    }
    public void SaveGame() {
        GameController.instance.SaveGame();
    }
    public void NewGame() {
        GameController.instance.NewGame();
    }



    private void SetScoreText(int value) {
        UpdateText(_scoreText, $"{value}");
    }

    private void SetMatchesText(int value) {
        UpdateText(_matchesText, $"{value}/{pairs}");
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