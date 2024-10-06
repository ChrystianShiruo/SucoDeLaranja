using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {

    [Header("Board")]
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private RectTransform BoardPanel;


    [Header("Counters")]
    [SerializeField] TextMeshProUGUI _turnsText;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _matchesText;

    //    private int _turns;
    //private int _score;
    //private int _matches;
    private GameData _gameData;
    private int pairs;


    public void Init(GameData gameData) {
        _gameData = gameData;
        InstantiateBoard(_gameData.Board);
        pairs = (_gameData.Board.GetLength(0) * _gameData.Board.GetLength(1)) / 2;
        _gameData.OnScoreChange += SetScoreText;
        _gameData.OnTurnsChange += SetTurnsText;
        _gameData.OnMatchesChange += SetMatchesText;


    }

    private void SetTurnsText(int value) {
        UpdateText(_turnsText, $"{value}");
    }

    private void SetScoreText(int value) {
        UpdateText(_scoreText, $"{value}");
    }

    private void SetMatchesText(int value) {
        UpdateText(_matchesText, $"{value}/{pairs}");
    }

    private void UpdateText(TextMeshProUGUI textComponent, string value) {
        textComponent.text = value;
    }

    //private Action<int> UpdateLabel(Text matchesText, int a) {

    //    throw new NotImplementedException();
    //}

    private void InstantiateBoard(CardState[,] board) {

        int width = board.GetLength(0);
        int height = board.GetLength(1);

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                GameObject go = Instantiate(_cardPrefab, BoardPanel);
                go.GetComponent<Card>().Init(board[x, y]);
            }
        }
    }
}