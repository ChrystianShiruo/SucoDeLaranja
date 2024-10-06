using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {

    [Header("Board")]
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private RectTransform _boardPanel;

    [Header("Counters")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _matchesText;
    [SerializeField] private TextMeshProUGUI _turnsText;

    private int pairs;
    private GameData _gameData;
    private GridLayoutGroup _boardPanelLayoutGroup;


    public void Init(GameData gameData) {
        _gameData = gameData;
        _boardPanelLayoutGroup = _boardPanel.GetComponent<GridLayoutGroup>();
        _boardPanelLayoutGroup.constraintCount = _gameData.Board.GetLength(1);

        InstantiateCards(_gameData.Board);
        SetCardsSize(_boardPanel, new Vector2Int(_gameData.Board.GetLength(0), _gameData.Board.GetLength(1)));

        pairs = (_gameData.Board.GetLength(0) * _gameData.Board.GetLength(1)) / 2;
        _gameData.OnScoreChange += SetScoreText;
        _gameData.OnTurnsChange += SetTurnsText;
        _gameData.OnMatchesChange += SetMatchesText;

        UpdateCounters(gameData);
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


    private void SetCardsSize(RectTransform boardPanel, Vector2Int dimensions) {
        float x = boardPanel.rect.width;
        float y = boardPanel.rect.height;
        Debug.Log($"{x}, {y}");
        x = x / dimensions.x;
        y = y / dimensions.y;
        _boardPanelLayoutGroup.cellSize = new Vector2(x, y);
    }

    private void InstantiateCards(CardState[,] board) {

        int width = board.GetLength(0);
        int height = board.GetLength(1);

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                GameObject go = Instantiate(_cardPrefab, _boardPanel);
                go.GetComponent<Card>().Init(board[x, y]);
            }
        }
    }
}