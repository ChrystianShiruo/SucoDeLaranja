using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {

    public CardInstanceArray[] Board { get => _board; }

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

    [SerializeField] private CardInstanceArray[] _board;
    [SerializeField] private Vector2Int _dimensions;
    [SerializeField] private int _turns;
    [SerializeField] private int _score;
    [SerializeField] private int _matches;
    [SerializeField] private int _comboLevel;

    public Action<int> OnTurnsChange;
    public Action<int> OnScoreChange;
    public Action<int> OnMatchesChange;


    public GameData(List<CardData> cards, Vector2Int dimensions) {
        BuildNewBoard(FillCardMatrix(cards, dimensions));

        _turns = 0;
        _score = 0;
        _matches = 0;
        _comboLevel = 0;
    }

    public void AddScore() {
        Score += 1 + _comboLevel;
        _comboLevel++;
    }

    public void ResetCombo() {
        _comboLevel = 0;
    }


    private void BuildNewBoard(CardData[,] cards) {
        _dimensions = new Vector2Int(cards.GetLength(0), cards.GetLength(1));
        _board = new CardInstanceArray[_dimensions.x];

        for(int x = 0; x < _dimensions.x; x++) {
            CardInstance[] c = new CardInstance[_dimensions.y];
            _board[x] = new CardInstanceArray();
            _board[x].cardArray = c;
            for(int y = 0; y < _dimensions.y; y++) {
                _board[x].cardArray[y] = new CardInstance(cards[x, y]);
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

}
[System.Serializable]
public class CardInstanceArray {
    public CardInstance[] cardArray;

}