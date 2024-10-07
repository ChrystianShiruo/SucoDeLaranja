using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public enum GameState {
        None,
        SettingUp,
        Playing,
        Finished
    }

    public GameData GameData { get => _gameData; }
    public GameState CurrentState { get => _currentState; }

    [SerializeField] private List<CardData> _cardPool;
    [SerializeField] private UIController _uiController;
    [SerializeField] private CardsManager _cardsManager;
    [SerializeField] private Vector2Int _boardDimensions; //TODO: validate that x*y is not an odd number
    [SerializeField] private float _preGameTime;

    private Dictionary<int, CardData> _cardDic;
    private GameState _currentState;
    private Card[,] _cards;
    private GameData _gameData;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
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
        yield return HideCards(_preGameTime);

        yield return null;
        //initialize player and player interaction
    }

    private IEnumerator HideCards(float time) {
        yield return new WaitForSeconds(time);
        _cardsManager.FlipAllCards();
        _currentState = GameState.Playing;
    }


    private void CreateGameData(Vector2Int layout, List<CardData> cardPool) {
        List<CardData> tempPool = new List<CardData>(cardPool);

        //<<Shuffling pool to select random cards for next game
        int pairs = (layout.x * layout.y) / 2;
        tempPool.Shuffle();//shuffling cards;
        tempPool.RemoveRange(pairs, (tempPool.Count - pairs));//picking the first n shuffled cards to use on this game;
        //>>
        //<<Adding selected cards pair and shuffling list for random positions;
        tempPool.AddRange(tempPool);
        tempPool.Shuffle();
        //>>
        //instantiate setup grid
        _gameData = new GameData(tempPool, layout);

    }

    public void IncrementTurn() {
        _gameData.Turns++;
    }

    public void PairScored() {
        //paired! +matches; +score; +combo
        _gameData.AddScore();
        _gameData.Matches++;

    }
    public void PairFailed() {
        //not paired; reset combo; flip cards to back again
        _gameData.ResetCombo();
    }
}
