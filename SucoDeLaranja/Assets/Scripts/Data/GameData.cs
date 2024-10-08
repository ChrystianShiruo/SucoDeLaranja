using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {

    public CardInstanceArray[] Board { get => _board; }

    public int Turns {
        set {
            turns = value;
            OnTurnsChange?.Invoke(value);
        }
        get => turns;
    }
    public int Score {
        set {
            score = value;
            OnScoreChange?.Invoke(value);
        }
        get => score;
    }
    public int Matches {
        set {
            matches = value;
            OnMatchesChange?.Invoke(value);
        }
        get => matches;
    }

    public CardInstanceArray[] _board;
    public Vector2Int _dimensions;
    public int turns;
    public int score;
    public int matches;
    public int comboLevel;

    public Action<int> OnTurnsChange;
    public Action<int> OnScoreChange;
    public Action<int> OnMatchesChange;


    public GameData(List<CardData> cards, Vector2Int dimensions) {
        BuildNewBoard(FillCardMatrix(cards, dimensions));

        turns = 0;
        score = 0;
        matches = 0;
        comboLevel = 0;
    }

    public void AddScore() {
        Score += 1 + comboLevel;
        comboLevel++;
    }

    public void ResetCombo() {
        comboLevel = 0;
    }


    private void BuildNewBoard(CardData[,] cards) {
        _dimensions = new Vector2Int(cards.GetLength(0), cards.GetLength(1));
        _board = new CardInstanceArray[_dimensions.x];
        //_board2 = new List<CardInstanceArray>();
        //CardInstanceArray c = new CardInstanceArray();
        //c.cardArray = new CardInstance[3] { new CardInstance(cards[0, 0]), new CardInstance(cards[0, 1]), new CardInstance(cards[2, 2]) };
        //_board2.Add(c);
        //_board2.Add(c);
        for(int x = 0; x < _dimensions.x; x++) {
            Debug.Log($"{x},? || {_dimensions}");
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