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
    [SerializeField] private Vector2Int _boardDimensions;
    [SerializeField] private float _preGameTime;

    private Dictionary<int, CardData> _cardDic;
    private GameState _currentState;
    private Card[,] _cards;
    private GameData _gameData;

    #region MonoBehaviourMethods
    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        _currentState = GameState.None;
        _cardDic = SetCardDictionary(_cardPool);
    }
    #region Coroutines
    private IEnumerator InitializeNewGame(Vector2Int layout) {
        Debug.Log("b");

        _currentState = GameState.SettingUp;
        //setup board
        CreateGameData(layout, _cardPool);

        //show all cards
        _uiController.Init(_gameData);
        _cardsManager.Init(_gameData);
        //flip cards
        Debug.Log("c");

        yield return HideCards(_preGameTime);
        Debug.Log("d");

        //allow player interaction with the game
        _currentState = GameState.Playing;
    }

    private IEnumerator LoadSavedGame(GameData gameData) {
        _currentState = GameState.SettingUp;
        _gameData = gameData;

        //show all cards
        _uiController.Init(_gameData);
        _cardsManager.Init(_gameData);
        //flip cards
        yield return HideCards(_preGameTime);
        //allow player interaction with the game


        yield return null;

        _currentState = GameState.Playing;
    }

    private IEnumerator HideCards(float time) {
        Debug.Log($"c2 {time}");

        yield return new WaitForSeconds(time);
        Debug.Log($"c3 {time}");

        _cardsManager.FlipAllCards();
    }



    #endregion
    #endregion

    public void SaveGame() {
        if(_currentState != GameState.Playing) {
            return;
        }
        DataManager.SaveGameDataJson(_gameData);
        //TODO: ui for successful save confirmation
    }

    public void LoadGame() {
        _gameData = DataManager.LoadGameDataJson();
        //TODO: reload game with loaded data
        StartCoroutine(LoadSavedGame(_gameData));

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


    #region Game Setup
    public bool NewGame() {
        //validate that x*y is not an odd number
        if((_boardDimensions.x * _boardDimensions.y) % 2 != 0) {
            return false;
        }
        Debug.Log("a");
        StartCoroutine(InitializeNewGame(_boardDimensions));
        return true;
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
    #endregion

}
