using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public Action<GameState> OnChangeGameState;

    public enum GameState {
        None,
        SettingUp,
        Playing,
        Finished
    }

    public GameData GameData { get => _gameData; }
    public GameState CurrentState {
        get => _currentState;
        private set {
            _currentState = value;
            OnChangeGameState?.Invoke(_currentState);
        }
    }

    [SerializeField] private List<CardData> _cardPool;
    [SerializeField] private UIController _uiController;
    [SerializeField] private CardsManager _cardsManager;
    [SerializeField] private Vector2Int _boardDimensions;
    [SerializeField] private float _preGameTime;

    private GameState _currentState;
    private Dictionary<int, CardData> _cardDic;
    private Card[,] _cards;
    private GameData _gameData;
    private Coroutine _currentRoutine;
    #region MonoBehaviourMethods
    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        CurrentState = GameState.None;
        _cardDic = SetCardDictionary(_cardPool);
    }
    #region Coroutines
    private IEnumerator InitializeNewGame(Vector2Int layout) {
        CurrentState = GameState.SettingUp;
        //setup board
        CreateGameData(layout, _cardPool);

        //show all cards
        _uiController.Init(_gameData);
        _cardsManager.Init(_gameData);

        //load card states (CardStatePreGame)
        _cardsManager.LoadAllCardStates();

        //flip cards
        yield return HideCards(_preGameTime);

        //allow player interaction with the game
        CurrentState = GameState.Playing;
    }

    private IEnumerator LoadSavedGame(GameData gameData) {
        
        CurrentState = GameState.SettingUp;

        //retrieve board
        //_gameData = new GameData(gameData);
        _gameData = new GameData(gameData);

        //show all cards
        _uiController.Init(_gameData);
        _cardsManager.Init(_gameData);
        //load card states
        _cardsManager.LoadAllCardStates();
        //yield return HideCards(_preGameTime);
        //allow player interaction with the game


        yield return null;

        CurrentState = GameState.Playing;
    }

    private IEnumerator HideCards(float time) {

        yield return new WaitForSeconds(time);

        _cardsManager.FlipAllCards();
    }



    #endregion
    #endregion

    #region Save/Load
    public void SaveGame() {
        if(CurrentState != GameState.Playing) {
            return;
        }
        DataManager.SaveGameDataJson(_gameData);
        //TODO: ui for successful save confirmation
    }

    public void LoadGame() {
        _gameData = DataManager.LoadGameDataJson();
        if(_currentRoutine != null) {
            StopCoroutine(_currentRoutine);
        }
        _currentRoutine = StartCoroutine(LoadSavedGame(_gameData));
    }
    #endregion

    public void IncrementTurn() {
        _gameData.Turns++;
    }
    public void PairScored() {
        _gameData.AddScore();
        _gameData.Matches++;

    }
    public void PairFailed() {
        _gameData.ResetCombo();
    }


    #region Game Setup
    public bool NewGame() {
        //validate that x*y is not an odd number
        if((_boardDimensions.x * _boardDimensions.y) % 2 != 0) {
            return false;
        }
        if(_currentRoutine != null) {
            StopCoroutine(_currentRoutine);
        }
        _currentRoutine = StartCoroutine(InitializeNewGame(_boardDimensions));
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
