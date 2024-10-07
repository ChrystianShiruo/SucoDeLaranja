using System;
using System.Collections.Generic;
using UnityEngine;

public class GameData {

    public CardInstance[,] Board { get => _board; }
    public int Turns {
        set {
            _turns = value;
            OnTurnsChange?.Invoke(value);
        }
        get => _turns;
    }
    public int Score {
        set {
            _score = value;
            OnScoreChange?.Invoke(value);
        }
        get => _score;
    }
    public int Matches {
        set {
            _matches = value;
            OnMatchesChange?.Invoke(value);
        }
        get => _matches;
    }

    private CardInstance[,] _board;
    private int _turns;
    private int _score;
    private int _matches;
    private int _comboLevel;

    public Action<int> OnTurnsChange;
    public Action<int> OnScoreChange;
    public Action<int> OnMatchesChange;


    public GameData(List<CardData> cards, Vector2Int dimensions) {
        BuildBoard(FillCardMatrix(cards, dimensions));

        _turns = 0;
        _score = 0;
        _matches = 0;
        _comboLevel = 0;
    }

    private void BuildBoard(CardData[,] cards) {
        _board = new CardInstance[cards.GetLength(0), cards.GetLength(1)];

        for(int x = 0; x < cards.GetLength(0); x++) {
            for(int y = 0; y < cards.GetLength(1); y++) {
                _board[x, y] = new CardInstance(cards[x, y]);
            }
        }
    }

    /// <summary>
    /// Create a card matrix from a card list and 2 dimensions
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="dimensions"></param>
    /// <returns></returns>
    private CardData[,] FillCardMatrix(List<CardData> cards, Vector2Int dimensions) {
        CardData[,] cardMatrix = new CardData[dimensions.x, dimensions.y];

        int c = 0;
        for(int x = 0; x < dimensions.x; x++) {
            for(int y = 0; y < dimensions.y; y++) {
                cardMatrix[x, y] = cards[c];
                c++;
            }
        }
        return cardMatrix;
    }

    public void AddScore() {
        Score += 1 + _comboLevel;
        _comboLevel++;
    }
    public void ResetCombo() {
        _comboLevel = 0;
    }
}