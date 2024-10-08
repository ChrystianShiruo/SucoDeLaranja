using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {



    [Header("Counters")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _matchesText;
    [SerializeField] private TextMeshProUGUI _turnsText;

    private int pairs;
    private GameData _gameData;



    private void OnDestroy() {
        _gameData.OnScoreChange -= SetScoreText;
        _gameData.OnTurnsChange -= SetTurnsText;
        _gameData.OnMatchesChange -= SetMatchesText;
    }

    public void Init(GameData gameData) {
        _gameData = gameData;


        pairs = (_gameData.Board.Length * _gameData.Board[0].cardArray.Length) / 2;
        _gameData.OnScoreChange += SetScoreText;
        _gameData.OnTurnsChange += SetTurnsText;
        _gameData.OnMatchesChange += SetMatchesText;

        UpdateCounters(_gameData);
    }

    public void LoadGame() {
        GameController.instance.LoadGame();
    }
    public void SaveGame() {
        GameController.instance.SaveGame();
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