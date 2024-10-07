using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public enum GameState {
        None,
        SettingUp,
        Playing,
        Finished
    }
    [SerializeField] private List<CardData> _cardPool;
    [SerializeField] private UIController _uiController;
    [SerializeField] private CardsManager _cardsManager;
    [SerializeField] private Vector2Int _boardDimensions; //TODO: validate that x*y is not an odd number

    private Dictionary<int, CardData> _cardDic;
    private GameData _gameData;
    private GameState _currentState;
    private Card[,] _cards;

    private void Awake() {
        _currentState = GameState.None;
        _cardDic = SetCardDictionary(_cardPool);
    }

    private void Start() {
        StartCoroutine(InitializeGame(_boardDimensions));
    }

    private Dictionary<int, CardData> SetCardDictionary(List<CardData> cardPool) {
        Dictionary<int, CardData> dic = new Dictionary<int, CardData>();
        foreach(CardData card in cardPool) {
            if(!dic.ContainsKey(card.id)) {
                dic.Add(card.id, card);
            }
        }
        return dic;
    }

    public IEnumerator InitializeGame(Vector2Int layout) {
        _currentState = GameState.SettingUp;
        //setup board
        CreateGameData(layout, _cardPool);

        _uiController.Init(_gameData);
        //show all cards
        _cardsManager.Init(_gameData);
        //flip cards
        yield return HideCards(2f);

        yield return null;
        //initialize player and player interaction
    }

    private IEnumerator HideCards(float time) {
        yield return new WaitForSeconds(time);
        _cardsManager.FlipAllCards();

    }


    private void CreateGameData(Vector2Int layout, List<CardData> cardPool) {
        List<CardData> tempPool = new List<CardData>(cardPool);

        //<<Shuffling pool to select random cards for next game
        int pairs = (layout.x * layout.y) / 2;
        tempPool.Shuffle();//shuffling cards;
        tempPool.RemoveRange(pairs, (tempPool.Count - pairs));//picking the first n shuffled cards to use on this game;

        //string s = String.Empty; //TODO: Remove
        //tempPool.ForEach(cardData => s += $"{cardData.id.ToString()}, ");
        //Debug.Log($"Selected cards for next game:\n{s}");
        //>>
        //<<Adding selected cards pair and shuffling list for random positions;
        tempPool.AddRange(tempPool);
        tempPool.Shuffle();

        //s = String.Empty; //TODO: Remove
        //tempPool.ForEach(cardData => s += $"{cardData.id.ToString()}, ");
        //Debug.Log($"Card order for next game:\n{s}");
        //>>
        //instantiate setup grid
        _gameData = new GameData(tempPool, layout);

    }


}
